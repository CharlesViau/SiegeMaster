using UnityEngine;

namespace Unit.Types
{
    public class Tower : Template.Unit
    {
        private Transform target;
        public ProjectileType projectiletype;
        public float projectileSpeed;
        public float projectileDamage;
        public Transform head;
        public Transform shootPos;
        public Transform SmokePosition;
        public ParticleType towerParticleType;


        public float timeToGetTarget;
        float timer;
        private void Start()
        {
            
        }
        private void Update()   
        {
            timer += Time.deltaTime;

            if (timer>timeToGetTarget)
            {
                target = Helper.GetCloset(typeof(EnemyManager), this.transform);
                timer=0;
            }

            if (target)
            {
                head.up = (target.position - head.position).normalized;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Fire();
                }
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
