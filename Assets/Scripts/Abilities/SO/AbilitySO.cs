using System;
using Abilities.AbilityState;
using General;
using Units.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities.SO
{
    public enum CastMethod
    {
        Indicator,
        QuickCastWithIndicator,
        QuickCast,
    }

    public abstract class AbilitySo : ScriptableObject
    {
        #region Properties and Variables

        [Header("Spell Stats")] public new string name;
        public int manaCost;
        public int range;

        public float baseCooldown;
        public float baseChannelTime;

        #region Cast Method

        [Header("Cast Method")] public CastMethod castMethod;

        [InspectorName("Lock Cast Method")]
        [Tooltip("Locking the cast method will force the ability to " +
                 "be cast a certain way, overriding player cast preference.")]
        [SerializeField]
        protected bool castMethodIsLock;

        public bool IsPressAndRelease => castMethod == CastMethod.QuickCastWithIndicator;

        #endregion

        #region State Specifics and StateMachine

        [Header("State Machine Parameter")]
        [SerializeField]
        [Tooltip("If ability is a toggle or has a recast mechanic, put this true")]
        private bool hasActiveState;

        //State Machine
        private AbilityStateMachine _stateMachine;

        //Ability State (Also written simplified bool to be use in ability handler)
        public bool IsReadyToCast => _stateMachine.IsReady;
        public bool IsChanneling => _stateMachine.IsChanneling;
        public bool IsTargeting => _stateMachine.IsTargeting;
        public bool IsActive => _stateMachine.IsActive;
        public bool IsOnCooldown => _stateMachine.IsOnCooldown;

        #endregion

        [Header("Ready State")] public Action OnReadyEnter;

        public UnityEvent onReadyExit;

        //[Header("Targeting State")]
        [Header("Channeling State")] public UnityEvent onChannelingEnter;
        //[Header("Active State")]

        //Public events related to "Input" Handling
        public bool IsPress { get; protected set; }
        public bool IsWaitingForAttackPressEvent { get; private set; }

        public Action OnPress;
        public Action OnRelease;
        public Action OnAttackPress;

        protected Unit Owner;
        protected Transform Target;

        public Targeting_SO targetingSo;

        #endregion

        public virtual void Init(Unit owner)
        {
            Owner = owner;

            _stateMachine = new AbilityStateMachine(this, OnCast, OnActiveCast);

            OnPress = () => { IsPress = true; };

            OnRelease = () => { IsPress = false; };
        }

        public void Refresh()
        {
            _stateMachine.Refresh();
        }

        protected abstract void ReadyStateRefresh();
        protected abstract void OnCast();
        protected abstract void OnActiveCast();

        private class AbilityStateMachine : StateMachine
        {
            #region Properties and Variables

            //States
            private readonly AbilityReadyState _readyState;
            private readonly AbilityTargetingState _targetingState;
            private readonly AbilityChannelingState _channelingState;
            private readonly AbilityCooldownState _cooldownState;
            private readonly AbilityActiveState _activeState;

            //Simple bool for State Check
            public bool IsReady => CurrentState == _readyState;
            public bool IsTargeting => CurrentState == _targetingState;
            public bool IsChanneling => CurrentState == _channelingState;
            public bool IsOnCooldown => CurrentState == _cooldownState;
            public bool IsActive => CurrentState == _activeState;

            #endregion

            public AbilityStateMachine(AbilitySo abilitySo, Action onCast, Action onActiveCast)
            {
                #region State Creation (Bunch of "state = new state()")

                _readyState = new AbilityReadyState(abilitySo);
                _targetingState = new AbilityTargetingState();
                _channelingState = new AbilityChannelingState(abilitySo, onCast);
                _cooldownState = new AbilityCooldownState(abilitySo);
                _activeState = new AbilityActiveState(abilitySo, onActiveCast);

                #endregion

                #region Transitions (from, to, Condition)

                //Ready 
                AddTransition(_readyState, _targetingState, () => abilitySo.IsPress);
                //Targeting
                AddTransition(_targetingState, _channelingState, HasTarget());
                //TODO: AddTransition(_targetingState, _readyState, () => ); // If spell cancel or switch spell before casting
                //Channeling
                AddTransition(_channelingState, _activeState, ChannelCompleteAndHasActiveState());
                AddTransition(_channelingState, _cooldownState, WasInterruptedOrChannelCompletedAndHasNoActiveState());
                //Active
                //AddTransition(_activeState, _cooldownState, () => );
                //Cooldown
                AddTransition(_cooldownState, _readyState, CooldownIsOver());

                #endregion

                #region Conditions

                Func<bool> HasTarget() => () => abilitySo.Target != null;

                Func<bool> ChannelCompleteAndHasActiveState() =>
                    () => _channelingState.HasCompleted && abilitySo.hasActiveState;

                Func<bool> WasInterruptedOrChannelCompletedAndHasNoActiveState() => () =>
                    _channelingState.HasBeenInterrupt ||
                    _channelingState.HasCompleted && !abilitySo.hasActiveState;

                Func<bool> CooldownIsOver() => () => _cooldownState.CooldownIsOver;

                #endregion

                SetState(_readyState);
            }
        }
    }
}