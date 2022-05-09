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
        const float enemyDamageToNexus = 1;
        #endregion

        #region UI & HP
        public Canvas canvasParent;
        int fullHP;
        public int currentHP;
        Stack<HP> hpStack;
        Vector3 facingDirUI;
        #endregion
        #endregion

        #region Methods
        #region Game Control & Flow
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
                    EnemyManager.Instance.Remove(this);
                    ObjectPool.Instance.Pool(enemyType, this);
                    delayToPool = 10;
                }
            }
        }

        public override void FixedRefresh()
        {

        }
        #endregion

        #region Factory & Pool manage
        public override void Pool()
        {
            base.Pool();
            gameObject.SetActive(false);

        }

        public override void Depool()
        {
            base.Depool();
            gameObject.SetActive(true);

        }

        public void Construct(Args constructionArgs)
        {
            transform.SetParent(constructionArgs.parent);
            currentHP = fullHP;
            alive = true;
            delayToPool = 10;
            enemyAgent.Move(constructionArgs.spawningPosition);
            hpStack.Clear();
            CreateHp(fullHP);
        }

        public class Args : ConstructionArgs
        {
            public Transform parent;
            public Args(Vector3 _spawningPosition, Transform _parent) : base(_spawningPosition)
            {
                parent = _parent;
            }
        }
        #endregion

        #region Movement
        public override void Move(Vector3 direction)
        {
            if (direction != null)
                movement_SO.MoveToPoint(direction);
        }
        #endregion

        #region Damage & Death manage
        public bool debugTest;
        public void GotShot(float damage)
        {
            if (debugTest)
                Debug.Log("");


            if (currentHP >= 0)
            {
                damage = Mathf.Clamp(damage, 0, currentHP);
                currentHP -= (int)damage;
                for (int i = 0; i < damage; i++)
                {
                    ObjectPool.Instance.Pool(HPType.EnemyHp, hpStack.Pop());
                }
            }
            DeathAnimation();
            //GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }


        void DeathAnimation()
        {
            if (currentHP <= 0)
            {
                alive = false;
                anim.SetTrigger("IsDead");
            }
        }
        #endregion

        #region HP & UI manage
        void CreateHp(int numberOfHp)
        {
            for (int i = 0; i < numberOfHp; i++)
            {
                hpStack.Push(HPManager.Instance.Create(HPType.EnemyHp, new HP.Args(Vector3.zero, canvasParent.transform)));
            }
            /*if (canvasParent.transform.childCount > 5)
                Debug.LogError("Fuckin shit");*/
        }
        void FacingUIToPlayer()
        {
            facingDirUI = player.transform.position - transform.position;
            canvasParent.transform.forward = facingDirUI;
        }
        #endregion

        #region Attacking Player & Nexus
        void InstantiateProjectile(Transform target)
        {
            ProjectileManager.Instance.Create(projectiletype,
                new Projectile.Args(transform.position, projectiletype,
                target, projectileSpeed, projectileDamage, Vector3.zero));
        }

        void Shoot()
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                InstantiateProjectile(player);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Nexus")
            {
                GotShot(currentHP);
                delayToPool = 0;
                NexusManager.Instance.DealDamage(enemyDamageToNexus);
            }
        }
        #endregion

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
        #endregion
    }
}