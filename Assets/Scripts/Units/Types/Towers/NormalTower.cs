using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;
namespace Units.Types
{
    public class NormalTower : Tower
    {
        public float projectileSpeed;

        public override void Fire(Transform target)
        {
            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(ParticlePosition.position));
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(barrel.position, projectiletype, target, projectileSpeed, projectileDamage, Vector3.zero));
        }
        public override void Extrabehavior()
        {
            head.up = (target.position - head.position).normalized;
        }
    }
    
}