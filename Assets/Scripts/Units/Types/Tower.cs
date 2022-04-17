using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Tower : Unit
    {
        private Transform target;
        public ProjectileType projectiletype;
        public float projectileSpeed;
        public float projectileDamage;

        public ParticleType towerParticleType;
        public float towerAttackRange;

        public float timeToGetTarget;
        float timer;
        public Transform head;
        public Transform shootPos;
        public Transform SmokePosition;
        private void Start()
        {
            
        }
        private void Update()   
        {
            timer += Time.deltaTime;

            if (timer>timeToGetTarget)
            {
                target = Helper.GetClosetInRange(typeof(EnemyManager), this.transform,towerAttackRange);
                timer=0;
                if (target)
                {
                    head.up = (target.position - head.position).normalized;
                    Fire();
                }
                
            }

            if (target)
            {
                head.up = (target.position - head.position).normalized;
            }

            

        }

        public void Fire()
        {


            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(SmokePosition.position));
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(shootPos.position,target, projectileSpeed,projectileDamage));
            //projectile.damage_SO.damage = projectileDamage;
            ///pro.

        }
    }
}
