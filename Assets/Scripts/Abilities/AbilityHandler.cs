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

        private AbilitySo _basicAttackClone;

        [SerializeField] private AbilitySo[] abilities = new AbilitySo[NumberOfAbility];
        private readonly AbilitySo[] _abilitiesClone = new AbilitySo[NumberOfAbility];
        
        [SerializeField] private AbilitySo[] towers = new AbilitySo[NumberOfAbility];
        private readonly AbilitySo[] _towersClone = new AbilitySo[NumberOfAbility];


        public AbilitySo SelectedAbility { get; private set; }
        private bool _inBuildingMode;

        //Events
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

            //Abilities
            for (var i = 0; i < NumberOfAbility; i++)
            {
                if (!abilities[i]) continue;
                _abilitiesClone[i] = Instantiate(abilities[i]);
                _abilitiesClone[i].Init(_owner);
            }


            if (basicAttack == null) return;
            _basicAttackClone = Instantiate(basicAttack);
            _basicAttackClone.Init(_owner);

            //Towers
        }

        public void PostInit()
        {
            SelectedAbility = _basicAttackClone;
        }

        public void Refresh()
        {
            foreach (var ability in _abilitiesClone)
            {
                if (ability)
                    ability.Refresh();
            }
        }

        public void FixedRefresh()
        {
        }

        public void LateRefresh()
        {
        }

        public void ToggleBuildingMode()
        {
            _inBuildingMode = !_inBuildingMode;
        }

        #region Private Methods

        private void OnFirePressEvent()
        {
            SelectedAbility.OnFirePress?.Invoke();
        }

        private void OnAbilityReleaseEvent(int i)
        {
        }

        private void OnAbilityPressEvent(int i)
        {
            if (_abilitiesClone[i] is null || SelectedAbility.IsChanneling) return;

            if (!_inBuildingMode)
            {
                SelectedAbility = _abilitiesClone[i];
                _abilitiesClone[i].OnFirePress?.Invoke();
            }
            else
            {
                SelectedAbility = towers[i];
                towers[i].OnFirePress?.Invoke();
            }
        }

        #endregion
    }
}