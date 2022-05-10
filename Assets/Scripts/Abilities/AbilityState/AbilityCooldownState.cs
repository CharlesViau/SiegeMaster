using Abilities.AbilitySO;
using General;
using UnityEngine;

namespace Abilities.AbilityState
{
    public class AbilityCooldownState : IState
    {
        private readonly AbilitySo _ability;
        private float _cooldownTimeLeft;

        public bool CooldownIsOver => _cooldownTimeLeft <= 0;
        
        public AbilityCooldownState(AbilitySo ability)
        {
            _ability = ability;
        }
        
        public void Refresh()
        {
            _cooldownTimeLeft -= Time.deltaTime;
        }

        public void OnEnter()
        {
            _cooldownTimeLeft = _ability.stats.baseCooldown;
        }

        public void OnExit()
        {
            
        }
    }
}