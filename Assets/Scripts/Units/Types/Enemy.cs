using General;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        /*public Transform[] targets;
        int waypointCounter = 0;*/

        public EnemyType EnemyType;
        public EnemyMovement_SO movement_SO;
        public bool alive;

        protected Transform player;
        public ProjectileType projectiletype;
        public float projectileDamage;
        public float attackRange;
        public float projectileSpeed;

        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }

        public override void Init()
        {
            base.Init();
            alive = true;
            movement_SO = Instantiate(movement_SO);            
            movement_SO.Init(gameObject, player, speed);
            player = PlayerUnitManager.Instance.GetTransform;
            //Debug.Log("init enemy");
        }

        public override void PostInit()
        {
            base.PostInit();

        }

        public override void Refresh()
        {
            base.Refresh();
            Move(player.position);
            movement_SO.Refresh();
            //Shoot();      
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

        public void GotShot(float damage)
        {
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
           ObjectPool.Instance.Pool(EnemyType, this);
        }

        public override void Move(Vector3 direction)
        {
            movement_SO.MoveToPoint(direction,alive);
        }

        public void Construct(Args constructionArgs)
        {
            transform.GetComponent<NavMeshAgent>().Move(constructionArgs.spawningPosition);
            //transform.position = constructionArgs.spawningPosition;
        }

        /*void WaypointsCheck()
        {
            if (Vector3.Distance(targets[waypointCounter].position, player.position) < 0.1f)
            {
                waypointCounter++;
                Debug.Log(waypointCounter);
            }
            if (targets.Length <= waypointCounter)
            {
                waypointCounter = 0;
            }
            //movement_SO.MoveToPoint(targets[waypointCounter].position);
            //rb.velocity = 20 * (targets[waypointCounter].position - player.position).normalized;
        }*/

        void CreateProjectile(Transform target)
        {
            ProjectileManager.Instance.Create(projectiletype,
                new Projectile.Args(transform.position, projectiletype,
                target, projectileSpeed, projectileDamage, Vector3.zero));
        }

        void Shoot()
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                CreateProjectile(player);
            }
        }
    }
}