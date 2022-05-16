using System;
using Abilities.AbilitySO;
using General;
using Units.Types;
using UnityEngine;

namespace Abilities
{
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
            OnAttackPress = OnFirePressEvent;
            
            foreach (var ability in abilities)
            {
                if (!ability) continue;
                Instantiate(ability);
                ability.Init(_owner);
            }

        }
        
        public void PostInit()
        {
            _currentAbility = basicAttack;
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
        private void OnFirePressEvent()
        {
            _currentAbility.OnFirePress?.Invoke();
        }
        
        private void OnAbilityReleaseEvent(int i)
        {
            
        }

        private void OnAbilityPressEvent(int i)
        {
            if (IsChanneling || !abilities[i].IsReadyToCast) return;
            _currentAbility = abilities[i];
            abilities[i].OnFirePress?.Invoke();
        }
        
        #endregion
    }
}