using General;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        /*public Transform[] targets;
        int waypointCounter = 0;*/
        public bool alive;
        public EnemyType enemyType;
        public ProjectileType projectiletype;
        public EnemyMovement_SO movement_SO;
        public Targeting_SO targeting_SO;

        protected NavMeshAgent enemyAgent;
        Animator anim;
        protected Transform player;
        protected Transform objective;
        public float projectileDamage;
        public float attackRange;
        public float projectileSpeed;

        public Canvas canvasParent;
        int fullHP;
        public int currentHP;
        Stack<HP> hpStack;

        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }

        public override void Init()
        {
            base.Init();
            enemyAgent= GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();

            enemyAgent.speed = speed;
            fullHP = currentHP;
            alive = true;
            
            

            movement_SO = Instantiate(movement_SO);
            targeting_SO = Instantiate(targeting_SO);
            
            player = PlayerUnitManager.Instance.GetTransform;
            objective = NexusManager.Instance.GetTransform;
            
            movement_SO.Init(gameObject, objective, speed);
            targeting_SO.Init(gameObject, attackRange);
            
            hpStack = new Stack<HP>();
            CreateHp();


            //Debug.Log("init enemy");
        }

        public override void PostInit()
        {
            base.PostInit();
        }

        public override void Refresh()
        {
            base.Refresh();
            Move(targeting_SO.GetTheTarget().position);
            anim.SetFloat("Speed", speed);
            
            if (currentHP <= 0)
            {
                //anim.SetTrigger("IsDead");
                ObjectPool.Instance.Pool(enemyType, this);                
            }
            //DetectPlayer();
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
            
            currentHP = fullHP;
            CreateHp();
        }

        public void GotShot(float damage)
        {
            currentHP -= (int)damage;
            ObjectPool.Instance.Pool(HPType.EnemyHp, hpStack.Pop());
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }

        public override void Move(Vector3 direction)
        {
            movement_SO.MoveToPoint(direction, alive);
        }

        public void Construct(Args constructionArgs)
        {            
             enemyAgent.Move(constructionArgs.spawningPosition);
            //transform.position = constructionArgs.spawningPosition;
        }

        void CreateHp()
        {
            //fullHP = 0;
            for (int i = 0; i < fullHP; i++)
            {
                HP h = HPManager.Instance.Create(HPType.EnemyHp, new HP.Args(Vector3.zero, canvasParent.transform));
                hpStack.Push(h);
            }
        }


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
    }
}