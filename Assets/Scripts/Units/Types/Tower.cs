using General;
using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Tower : Unit,ICreatable<Tower.Args>
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
        public Transform barrel;
        public Transform SmokePosition;
        public override void Refresh()
        {
            base.Refresh();
            timer += Time.deltaTime;

            if (timer > timeToGetTarget)
            {
                target = Helper.GetClosetInRange(typeof(EnemyManager), this.transform, towerAttackRange);
                timer = 0;
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

        protected virtual void Fire()
        {
            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(SmokePosition.position));
            
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(barrel.position,target, projectileSpeed,projectileDamage,Vector3.zero));
            //projectile.damage_SO.damage = projectileDamage;
            ///pro.

        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
        }

        public class Args : ConstructionArgs
        {

            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {

            }

        }

    }
}
