using System;
using Abilities.AbilityState;
using General;
using SO.TowerSo.Targeting;
using Units.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities.SO
{
    public enum CastMethod
    {
        CantBeCast,
        Indicator,
        QuickCastWithIndicator,
        QuickCast,
    }

    public abstract class AbilitySo : ScriptableObject
    {
        #region Properties and Variables

        [Header("Stats")] public AbilityStats stats;

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

        public TargetingSo targetingSo;
        public GameObject target;

        #endregion

        #region Public Methods
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

        #endregion
        
        #region Abstract Methods

        protected abstract void ReadyStateRefresh();
        protected abstract void OnCast();
        protected abstract void OnActiveCast();

        #endregion

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
                _targetingState = new AbilityTargetingState(abilitySo);
                _channelingState = new AbilityChannelingState(abilitySo, onCast);
                _cooldownState = new AbilityCooldownState(abilitySo);
                _activeState = new AbilityActiveState(abilitySo, onActiveCast);

                #endregion

                #region Transitions (from, to, Condition)

                //Ready 
                AddTransition(_readyState, _targetingState, WasTrigger());
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

                Func<bool> WasTrigger() => () => abilitySo.IsPress;

                Func<bool> HasTarget() => () => abilitySo.target != null;

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

        [System.Serializable]
        public class AbilityStats
        {
            public string name;
            public int manaCost;
            public int maxRange;

            public float baseChannelTime;
            public float baseCooldown;
        }

        [System.Serializable]
        public class Event : UnityEvent
        {
            public AnimationClip animationClipToPlay;
            //TODO : Wwise Event Field 
            //TODO : Particle System 

            private Animator _animator;

            /*public void Init(AbilitySo abilitySo)
            {
                _animator = abilitySo.Owner.GetComponent<Animator>();
                if(animationClipToPlay) AddListener(()=> _animator.;
            }*/
        }
    }
}