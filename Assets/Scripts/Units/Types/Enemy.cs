using General;
using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        public EnemyType EnemyType;
        public EnemyMovement_SO movement_SO;
        public Transform[] targets;
        int waypointsCounter = 0;
        public bool alive;

        public Transform player;
        public ProjectileType projectiletype;
        public float projectileSpeed;
        public float projectileDamage;
        public float attackRange;
        
        public override void Init()
        {
            base.Init();
            alive = true;
            movement_SO = Instantiate(movement_SO);
            movement_SO.Init(gameObject, targets, speed);
            //Debug.Log("Enemy init");
        }

        public override void PostInit()
        {
            base.PostInit();
        }

        public override void Refresh()
        {
            base.Refresh();

            if (Vector3.Distance(transform.position, targets[waypointsCounter].position) <= 0.1f)
            {
                waypointsCounter++;
                //Debug.Log(waypointsCounter);
            }
            if (targets.Length <= waypointsCounter)
                waypointsCounter = 0;
            movement_SO.MoveToPoint(targets[waypointsCounter].position);

            Shoot();
        }

        public override void FixedRefresh()
        {

        }

        public override void Pool()
        {
            base.Pool();
            alive = false;
            gameObject.SetActive(false);
        }

        public override void Depool()
        {
            base.Depool();
            alive = true;
            gameObject.SetActive(true);
        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
        }

        public void GotShot(float damage)
        {
            ObjectPool.Instance.Pool(EnemyType, this);
        }

        public override void Move(Vector3 direction)
        {
            base.Move(direction);
        }

        public void CreateProjectile(Transform target)
        {
            ProjectileManager.Instance.Create(projectiletype, 
                new Projectile.Args(transform.position, target, 
                projectileSpeed, projectileDamage, Vector3.zero));
        }

        public void Shoot()
        {            
            if (Vector3.Distance(player.position, transform.position) <= attackRange)
                CreateProjectile(player);
        }

        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }
    }
}