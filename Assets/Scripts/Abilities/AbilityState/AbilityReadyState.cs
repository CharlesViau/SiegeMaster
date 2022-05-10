using Abilities.AbilitySO;
using General;

namespace Abilities.AbilityState
{
    public class AbilityReadyState : IState
    {
        private readonly AbilitySo _abilitySo;

        public AbilityReadyState(AbilitySo abilitySo)
        {
            _abilitySo = abilitySo;
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