using BattelObejcts;
using Managers;
using Units.Types;
using Units.Types.Towers;
using UnityEngine;

namespace Abilities.AbilitySO
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/SpellAbility")]
    public class SpellAbility : AbilitySo
    {
        public SpellType type;
        public float SphireCastRadius;


        protected override void ReadyStateRefresh()
        {
        }

        protected override void OnCast()
        {
            SpellManager.Instance.Create(type,new Spell.Args(TargetTransform.position, SphireCastRadius));
        }

        protected override void OnActiveCast()
        {
            
        }

        protected override void ActiveStateRefresh()
        {
            
        }
    }
}
