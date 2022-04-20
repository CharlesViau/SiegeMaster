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
        float timer = 0;
        public override void Refresh()
        {
            base.Refresh();
            timer += Time.deltaTime;

            if (timer > attackSpeed)
            {
                target = Helper.GetClosetInRange(typeof(EnemyManager), this.transform, towerAttackRange);
                timer = 0;
                if (target)
                {
                    head.up = (target.position - head.position).normalized;
                    Fire(target);
                }

            }

            if (target)
            {
                head.up = (target.position - head.position).normalized;
            }

        }
        public override void Fire(Transform target)
        {
            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(ParticlePosition.position));
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(barrel.position, target, projectileSpeed, projectileDamage, Vector3.zero));
        }


    }
}