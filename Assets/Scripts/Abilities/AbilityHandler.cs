using System;
using Abilities.SO;
using General;
using Units.Types;
using UnityEngine;

namespace Abilities
{
    [RequireComponent(typeof(Unit))]
    public class AbilityHandler : MonoBehaviour, IUpdatable
    {
        private Unit _owner;
        private const int NumberOfAbility = 4;

        [Header("Attack and Abilities")] [SerializeField]
        private AbilitySo basicAttack;

        [SerializeField] private AbilitySo[] abilities = new AbilitySo[NumberOfAbility];


        private AbilitySo _currentAbility;

        private bool IsChanneling => _currentAbility.IsChanneling;
        private bool IsPress => _currentAbility.IsPress;
        private bool IsReadyToCast => _currentAbility.IsReadyToCast;

        public Action<int> OnAbilityPress;
        public Action<int> OnAbilityRelease;
        public Action OnAttackPress;

        public void Init()
        {
            _owner = GetComponent<Unit>();
            
            //Init Events
            OnAbilityPress = OnAbilityPressEvent;
            OnAbilityRelease = OnAbilityReleaseEvent;
            OnAttackPress = OnAttackPressEvent;

        }
        
        public void PostInit()
        {
            
        }

        public void Refresh()
        {
            foreach (var ability in abilities)
            {
                if(ability) ability.Refresh();
            }
        }

        public void FixedRefresh()
        {
            
        }

        public void LateRefresh()
        {
          
            
        }
        
        #region Private Methods
        private void OnAttackPressEvent()
        {
            if  (_currentAbility == basicAttack && IsReadyToCast)
            {
                _currentAbility.OnAttackPress?.Invoke();
            }
            else if (_currentAbility.IsWaitingForAttackPressEvent)
            {
                _currentAbility.OnAttackPress?.Invoke();
            }
        }
        
        private void OnAbilityReleaseEvent(int i)
        {
            if (_currentAbility == abilities[i] && IsPress && abilities[i].IsPressAndRelease)
            {
                _currentAbility.OnRelease?.Invoke();
            }
        }

        private void OnAbilityPressEvent(int i)
        {
            if (IsChanneling || !abilities[i].IsReadyToCast) return;
            //TODO notify current spell we are changing to reset that spell state.
            _currentAbility = abilities[i];
            abilities[i].OnPress?.Invoke();
        }
        
        #endregion
    }
}