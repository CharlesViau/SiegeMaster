using UnityEngine;

namespace Unit.Types
{
    public class Tower : Template.Unit
    {
        public Transform target;
        public float projectileSpeed;
        public float projectileDamage;
        public GameObject bullet;
        public Transform head;
        public Transform shootPos;
        private ProjectileType projectiletype;
        private void Start()
        {
            projectiletype = bullet.GetComponent<Projectile>().type;  
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

           
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(target, shootPos.position, projectileSpeed,projectileDamage));
            //projectile.damage_SO.damage = projectileDamage;
            ///pro.

        }
    }
}
