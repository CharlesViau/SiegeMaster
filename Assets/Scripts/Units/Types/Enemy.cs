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
        #region Fields
        /*public Transform[] targets;
        int waypointCounter = 0;*/

        public bool alive;
        float delayToPool = 10;

        #region Set Enemy Type
        public EnemyType enemyType;
        public EnemyMovement_SO movement_SO;
        public Targeting_SO targeting_SO;
        #endregion

        #region Get Components
        protected NavMeshAgent enemyAgent;
        protected Animator anim;
        protected Transform player;
        protected Transform objective;
        #endregion

        #region Attacking
        public ProjectileType projectiletype;
        public float projectileDamage;
        public float attackRange;
        public float projectileSpeed;
        #endregion

        #region UI & HP
        public Canvas canvasParent;
        int fullHP;
        public int currentHP;
        Stack<HP> hpStack;
        Vector3 facingDirUI;
        #endregion
        #endregion

        public override void Init()
        {
            base.Init();
            enemyAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();

            alive = true;
            enemyAgent.speed = speed;
            fullHP = currentHP;

            movement_SO = Instantiate(movement_SO);
            targeting_SO = Instantiate(targeting_SO);

            player = PlayerUnitManager.Instance.GetTransform;
            objective = NexusManager.Instance.GetTransform;

            movement_SO.Init(gameObject, objective, speed);
            targeting_SO.Init(gameObject, attackRange);

            hpStack = new Stack<HP>();
            
         
        }

        public override void PostInit()
        {
            base.PostInit();
        }

        public override void Refresh()
        {
            base.Refresh();
            if (alive)
            {
                anim.SetFloat("Speed", speed);
                Move(targeting_SO.GetTheTarget().position);
                FacingUIToPlayer();                
                //Shoot();
            }

            if (!alive)
            {
                if (gameObject.activeInHierarchy)
                    enemyAgent.ResetPath();
                delayToPool -= Time.deltaTime;
                if (delayToPool <= 0)
                {
                    ObjectPool.Instance.Pool(enemyType, this);
                    delayToPool = 10;
                }
            }
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
            gameObject.SetActive(true);

        }

        //public bool debugTest;
        public void GotShot(float damage)
        {
            currentHP -= (int)damage;

            if (currentHP >= 0)
                for (int i = 0; i < damage; i++)
                {
                    try
                    {
                        ObjectPool.Instance.Pool(HPType.EnemyHp, hpStack.Pop());
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("Out of range");
                        throw;
                    }
                }
            DeathAnimation();

            /*if (debugTest)
                Debug.Log("");*/
            //GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }

        public override void Move(Vector3 direction)
        {
            if (direction != null && alive)
                movement_SO.MoveToPoint(direction);
        }

        public void Construct(Args constructionArgs)
        {  
            transform.SetParent(constructionArgs.parent);
            enemyAgent.Move(constructionArgs.spawningPosition);
            alive = true;
            delayToPool = 10;
            //nemyAgent.Move(spawn);
            currentHP = fullHP;
            hpStack.Clear();
            CreateHp(fullHP);
            //transform.position = constructionArgs.spawningPosition;
        }

        void CreateHp(int numberOfHp)
        {
            for (int i = 0; i < numberOfHp; i++)
            {               
                hpStack.Push(HPManager.Instance.Create(HPType.EnemyHp, new HP.Args(Vector3.zero,canvasParent.transform)));
            }
        }

        void DeathAnimation()
        {
            if (currentHP <= 0)
            {
                alive = false;
                anim.SetTrigger("IsDead");
            }
        }

        void FacingUIToPlayer()
        {
            facingDirUI = player.transform.position - transform.position;
            canvasParent.transform.forward = facingDirUI;
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

        public class Args : ConstructionArgs
        {
            public Transform parent;
            public Args(Vector3 _spawningPosition, Transform _parent) : base(_spawningPosition)
            {
                parent = _parent;
            }
        }
    }
}