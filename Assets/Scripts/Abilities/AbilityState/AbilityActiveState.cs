using System;
using Abilities.SO;
using General;

namespace Abilities.AbilityState
{
    public class AbilityActiveState : IState
    {
        private AbilitySo _abilitySo;
        private Action _onActiveCast;
        public AbilityActiveState(AbilitySo abilitySo, Action onActiveCast)
        {
            _abilitySo = abilitySo;
            _onActiveCast = onActiveCast;
        }
        public void Refresh()
        {
            
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
           
        }
    }
}