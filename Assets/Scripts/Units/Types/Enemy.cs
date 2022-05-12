using General;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using SO.TowerSo.Targeting;
using System.Collections;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        #region Fields
        /*public Transform[] targets;
        int waypointCounter = 0;*/

        #region Set Enemy Type
        public EnemyType enemyType;
        public EnemyMovement_SO movement_SO;
        public TargetingSo targeting_SO;
        protected override Vector3 targetPosition
        {
            get
            {
                //TODO : return position of the current objective?
                return Vector3.zero;
            }
        }
        #endregion

        #region Get Components
        private NavMeshAgent _enemyAgent;
        private Transform _player;
        private Transform _objective;
        #endregion

        #region Death
        public bool alive;
        float _delayToPool = 10;
        #endregion

        #region Attacking
        public ProjectileType projectileType;
        public float projectileDamage;
        public float attackRange;
        public float ShootRange;
        public float FightRange;
        public float projectileSpeed;
        const float EnemyDamageToNexus = 1;
        #endregion

        #region Animation
        static readonly int Speed = Animator.StringToHash("Speed");
        static readonly int IsDead = Animator.StringToHash("IsDead");
        static readonly int IsAttack = Animator.StringToHash("IsAttack");
        static readonly int IsFight = Animator.StringToHash("IsFight");
        #endregion

        #region UI & HP
        public Canvas canvasParent;
        public int currentHp;
        int _fullHp;
        Stack<Hp> _hpStack;
        Vector3 _facingDirUI;
        #endregion
        #endregion

        #region Methods
        #region Game Control & Flow
        public override void Init()
        {
            base.Init();
            _enemyAgent = GetComponent<NavMeshAgent>();
            alive = true;
            _enemyAgent.speed = speed;

            _fullHp = currentHp;

            movement_SO = Instantiate(movement_SO);
            targeting_SO = Instantiate(targeting_SO);

            _player = PlayerUnitManager.Instance.GetTransform;
            _objective = NexusManager.Instance.GetTransform;

            movement_SO.Init(gameObject, _objective, speed);
            targeting_SO.Init(gameObject, attackRange);

            _hpStack = new Stack<Hp>();
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
                Animator.SetFloat(Speed, speed);
                Move(targeting_SO.GetTheTarget().position);
                //FacingUIToPlayer();
                //Shoot();
            }

            if (!alive)
                Death();
        }

        public override void FixedRefresh()
        {

        }
        #endregion

        #region Factory & Pool manage
        public class Args : ConstructionArgs
        {
            public readonly Transform Parent;
            public Args(Vector3 _spawningPosition, Transform parent) : base(_spawningPosition)
            {
                Parent = parent;
            }
        }

        public void Construct(Args constructionArgs)
        {
            transform.SetParent(constructionArgs.Parent);
            currentHp = _fullHp;
            alive = true;
            _delayToPool = 10;
            _enemyAgent.Move(constructionArgs.spawningPosition);
            _hpStack.Clear();
            CreateHp(_fullHp);
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
        #endregion

        #region Movement
        public override void Move(Vector3 direction)
        {
            if (direction != Vector3.zero)
                movement_SO.MoveToPoint(direction);
        }
        #endregion

        #region Damage & Death manage
        public void GotShot(float damage)
        {
            if (currentHp >= 0)
            {
                damage = Mathf.Clamp(damage, 0, currentHp);
                currentHp -= (int)damage;

                for (var i = 0; i < damage; i++)
                {
                    var h = _hpStack.Pop();
                    h.transform.SetParent(null);
                    HPManager.Instance.Pool(HPType.EnemyHp, h);
                }
            }
            if (alive && currentHp <= 0)
                Death();
            //DeathAnimation();
            //if(!alive)
        }

        private void DeathAnimation()
        {
            Animator.SetTrigger(IsDead);
            //Death();
        }

        void Death()
        {
            if (gameObject.activeInHierarchy)
                _enemyAgent.ResetPath();

            _delayToPool -= Time.deltaTime;
            if (_delayToPool <= 0)
            {
                EnemyManager.Instance.Pool(enemyType, this);
            }

            alive = false;
            DeathAnimation();
        }
        #endregion

        #region HP & UI manage

        private void CreateHp(int numberOfHp)
        {
            for (int i = 0; i < numberOfHp; i++)
            {
                _hpStack.Push(HPManager.Instance.Create(HPType.EnemyHp, new Hp.Args(Vector3.zero, canvasParent.transform)));
            }
        }

        private void FacingUIToPlayer()
        {
            _facingDirUI = _player.transform.position - transform.position;
            canvasParent.transform.forward = _facingDirUI;
        }
        #endregion

        #region Attacking Player & Nexus

        private void InstantiateProjectile(Transform target)
        {
            //Vector3 offset = new Vector3(0, 0, 5);
            ProjectileManager.Instance.Create(projectileType,
                new Projectile.Args((transform.position), projectileType,
                target, projectileSpeed, projectileDamage, Vector3.zero,false));
        }

        private void ShootAnimation()
        {
            Animator.SetTrigger(IsFight);
        }

        private void Shoot()
        {
            if (Vector3.Distance(transform.position, _player.position) <= attackRange)
            {
                InstantiateProjectile(_player);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Nexus")) return;
            GotShot(currentHp);
            _delayToPool = 0;
            NexusManager.Instance.DealDamage(EnemyDamageToNexus);
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