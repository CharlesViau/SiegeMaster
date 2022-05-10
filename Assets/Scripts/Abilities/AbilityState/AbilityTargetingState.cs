using System;
using Abilities.AbilitySO;
using Abilities.TargetingSO;
using General;
using UnityEngine;


namespace Abilities.AbilityState
{
    public class AbilityTargetingState : IState
    {
        private readonly TargetingSo _targetingSo;
        private readonly AbilitySo _abilitySo;
        public Action OnAttackEvent;

        public AbilityTargetingState(AbilitySo abilitySo)
        {
            _abilitySo = abilitySo;
            _targetingSo = abilitySo.targetingSo;

            OnAttackEvent = () =>
            {
                _abilitySo.Target = _targetingSo.TargetTransform;
            };
        }

        public void Refresh()
        {
            _targetingSo.Refresh();
            //TODO : Move UI to TargetingSO target location.
        }

        public void OnEnter()
        {
            _abilitySo.Target = null;
            //TODO: Create the Transparent 'UI' to indicate where it will cast if there's UI.
        }

        public void OnExit()
        {
            
        }
    }
}