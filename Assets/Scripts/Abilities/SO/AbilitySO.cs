using System;
using Abilities.AbilityState;
using General;
using Units.Types;
using UnityEngine;

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
        [Header("Spell Stats")] public new string name;
        public int manaCost;
        public int range;

        public float baseCooldown;
        public float baseChannelTime;


        [Header("Cast Method")] public CastMethod castMethod;

        [InspectorName("Lock Cast Method")]
        [Tooltip("Locking the cast method will force the ability to " +
                 "be cast a certain way, overriding player cast preference.")]
        [SerializeField]
        protected bool castMethodIsLock;

        public bool IsPressAndRelease => castMethod == CastMethod.QuickCastWithIndicator;

        //State Machine
        private AbilityStateMachine _stateMachine;

        //Ability State (Also written simplified bool to be use in ability handler)
        public bool IsReadyToCast => _stateMachine.IsReady;
        public bool IsChanneling => _stateMachine.IsChanneling;
        public bool IsTargeting => _stateMachine.IsTargeting;
        public bool IsActive => _stateMachine.IsActive;
        public bool IsOnCooldown => _stateMachine.IsOnCooldown;


        //Public events related to "Input" Handling
        public bool IsPress { get; protected set; }
        public bool IsWaitingForAttackPressEvent { get; private set; }

        public Action OnPress;
        public Action OnRelease;
        public Action OnAttackPress;

        protected Unit Owner;
        protected Transform Target;

        public Targeting_SO targetingSo;

        public virtual void Init(Unit owner)
        {
            Owner = owner;

            _stateMachine = new AbilityStateMachine(this);
        }

        public void Refresh()
        {
            _stateMachine.Refresh();
        }

        protected abstract void ReadyStateRefresh();
        protected abstract void OnCast();

        private class AbilityStateMachine : StateMachine
        {
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

            public AbilityStateMachine(AbilitySo abilitySo)
            {
                _readyState = new AbilityReadyState();
                _targetingState = new AbilityTargetingState();
                _channelingState = new AbilityChannelingState();
                _cooldownState = new AbilityCooldownState(abilitySo);
                _activeState = new AbilityActiveState();

                //Ready 
                AddTransition(_readyState, _targetingState, AbilityIsPress());
                //Targeting
                //AddTransition(_targetingState, _channelingState, () => );
                //AddTransition(_targetingState, _readyState, () => ); // If spell cancel or switch spell.


                AddTransition(_channelingState, _cooldownState,
                    () => _channelingState.ChannelTime >= abilitySo.baseChannelTime);

                //Conditions
                Func<bool> AbilityIsPress() => () => abilitySo.IsPress;
            }
        }
    }
}