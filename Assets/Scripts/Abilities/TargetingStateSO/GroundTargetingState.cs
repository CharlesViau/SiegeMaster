using Managers;
using UnityEngine;

namespace Abilities.TargetingStateSO
{
    [CreateAssetMenu(fileName = "GroundTargeting",
        menuName = "ScriptableObjects/Ability State/Targeting/Ground Targeting")]
    public class GroundTargetingState : AbilityTargetingStateSo
    {
        private SpellUI _spellUI;

        public override void Refresh()
        {
            TargetingSoClone.Refresh();
            _spellUI.transform.position = TargetingSoClone.TargetTransform.position;
        }

        public override void OnEnter()
        {
            TargetingSoClone.TargetTransform.position = AbilitySo.TargetPosition;
            
            if (spellUIType != SpellUIType.None)
                _spellUI = SpellUIManager.Instance.Create(spellUIType, new SpellUI.Args(AbilitySo.TargetPosition));
        }

        public override void OnExit()
        {
            if (spellUIType != SpellUIType.None)
                SpellUIManager.Instance.Pool(spellUIType, _spellUI);
        }

        protected override void OnFirePressEvent()
        {
            AbilitySo.TargetTransform = TargetingSoClone.TargetTransform;
        }

        protected override void OnFireReleaseEvent()
        {
        }
    }
}