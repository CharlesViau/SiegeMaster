using UnityEngine;

namespace Unit.Types
{
    public class Tower : Template.Unit
    {
        public Transform target;
        public ProjectileType projectiletype;
        public float projectileSpeed;
        public float projectileDamage;
        public Transform head;
        public Transform shootPos;
        public Transform SmokePosition;
        public ParticleType towerParticleType;
        private void Start()
        {
        }
        private void Update()
        {

            head.up = (target.position - head.position).normalized;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
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
