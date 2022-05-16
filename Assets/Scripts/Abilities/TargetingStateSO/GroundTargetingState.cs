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
            targetingSo.Refresh();
            _spellUI.transform.position = targetingSo.TargetTransform.position;
        }

        public override void OnEnter()
        {
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
            AbilitySo.TargetTransform = targetingSo.TargetTransform;
        }

        protected override void OnFireReleaseEvent()
        {
        }
    }
}