using Abilities.SO;
using General;
using UnityEngine;

namespace Abilities.AbilityState
{
    public class AbilityCooldownState : State
    {
        private readonly AbilitySo _ability;
        public float _cooldownTimeLeft { get; private set; }
        
        public AbilityCooldownState(AbilitySo ability)
        {
            _ability = ability;
        }
        
        public override void Refresh()
        {
            _cooldownTimeLeft -= Time.deltaTime;
        }

        public override void OnEnter()
        {
            _cooldownTimeLeft = _ability.baseCooldown;
        }

        public override void OnExit()
        {
            
        }
    }
}