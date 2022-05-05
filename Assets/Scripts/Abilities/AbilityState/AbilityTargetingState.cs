using Abilities.SO;
using General;
using SO.TowerSo.Targeting;

namespace Abilities.AbilityState
{
    public class AbilityTargetingState : IState
    {
        private TargetingSo _targetingSo;
        private readonly AbilitySo _abilitySo;

        public AbilityTargetingState(AbilitySo abilitySo)
        {
            _abilitySo = abilitySo;
            _targetingSo = abilitySo.targetingSo;
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}