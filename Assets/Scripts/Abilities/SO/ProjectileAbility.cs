using UnityEngine;

namespace Abilities.SO
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities/ProjectileAbility")]
    public class ProjectileAbility : AbilitySo
    {
        public ProjectileType type;
        public float projectileSpeed;
        public float projectileDamage;

        protected override void ReadyStateRefresh()
        {
            
        }

        protected override void OnCast()
        {
            ProjectileManager.Instance.Create(type,
                new Projectile.Args(Owner.transform.position, type, target.transform, projectileDamage, projectileSpeed, Vector3.zero));
        }

        protected override void OnActiveCast()
        {
            
        }
    }
}
