using Abilities.AbilitySO;
using UnityEngine;

namespace Abilities.AbilityStates
{
    public class AbilityCooldownState : AbilityStates.AbilityState
    {
        private float _cooldownTimeLeft;

        public bool CooldownIsOver => _cooldownTimeLeft <= 0;
        
        public AbilityCooldownState(AbilitySo ability) : base(ability)
        {
            
        }
        
        public override void Refresh()
        {
            _cooldownTimeLeft -= Time.deltaTime;
        }

        public override void OnEnter()
        {
            _cooldownTimeLeft = AbilitySo.stats.baseCooldown;
        }

        public override void OnExit()
        {
            
        }

        protected override void OnFirePressAction()
        {
        }

        protected override void OnFireReleaseAction()
        {
        }
    }
}