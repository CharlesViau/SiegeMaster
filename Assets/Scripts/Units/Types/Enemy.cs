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
    public enum EnemyStates { Wander, DeathAnimation, Death, Attacking }

    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        #region Fields
        #region Enemy states
        EnemyStates enemyState;
        #endregion

        #region Set Enemy Type
        public EnemyType enemyType;
        public EnemyMovement_SO movement_SO;
        public TargetingSo targeting_SO;
        public Attack_SO attack_SO;
        protected override Vector3 AimedPosition
        {
            get
            {
                //useless for enemies right now
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
        public float detectRange;
        public float attackRange;
        const float EnemyDamageToNexus = 1;
        #endregion

        #region Animation
        static readonly int Speed = Animator.StringToHash("Speed");
        static readonly int IsDead = Animator.StringToHash("IsDead");
        #endregion

        #region UI & HP
        public Canvas canvasParent;
        public Transform hpInPoolParent;
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
            _player = PlayerUnitManager.Instance.GetTransform;
            _objective = NexusManager.Instance.GetTransform;

            enemyState = EnemyStates.Wander;
            alive = true;
            _enemyAgent.speed = speed;

            _fullHp = currentHp;
            _hpStack = new Stack<Hp>();

            movement_SO = Instantiate(movement_SO);
            targeting_SO = Instantiate(targeting_SO);
            movement_SO.Init(gameObject, _objective, speed);
            targeting_SO.Init(gameObject, detectRange);
            if (attack_SO)
            {
                attack_SO = Instantiate(attack_SO);
                attack_SO.Init(ShootingPosition, _player);
            }
        }

        public override void PostInit()
        {
            base.PostInit();
        }

        public override void Refresh()
        {
            //base.Refresh();
            switch (enemyState)
            {
                case EnemyStates.Wander:
                    Move(targeting_SO.GetTheTarget().position);
                    FacingUIToPlayer();
                    GetReadyToAttack();
                    break;
                case EnemyStates.DeathAnimation:
                    DeathAnimation();
                    break;
                case EnemyStates.Death:
                    Death();
                    break;
                case EnemyStates.Attacking:
                    AttackState();
                    GetReadyToAttack();
                    break;
                default:
                    break;
            }
        }

        public override void FixedRefresh()
        {

        }
        #endregion

        #region Factory & Pool manage
        public class Args : ConstructionArgs
        {
            public Transform parent;
            public Args(Vector3 _spawningPosition, Transform parent) : base(_spawningPosition)
            {
                this.parent = parent;
            }
        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            _enemyAgent.enabled = true;
            transform.SetParent(constructionArgs.parent);
            enemyState = EnemyStates.Wander;
            currentHp = _fullHp;
            alive = true;
            _delayToPool = 10;
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
            Animator.SetFloat(Speed, speed);
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
                    h.transform.SetParent(hpInPoolParent);
                    HPManager.Instance.Pool(HPType.EnemyHp, h);
                }
            }
            if (alive && currentHp <= 0)
                enemyState = EnemyStates.DeathAnimation;
        }

        private void DeathAnimation()
        {
            if (gameObject.activeInHierarchy)
                _enemyAgent.ResetPath();

            Animator.SetTrigger(IsDead);
            alive = false;
            enemyState = EnemyStates.Death;
        }

        void Death()
        {
            StartCoroutine(DealyToPool());
        }

        IEnumerator DealyToPool()
        {
            yield return new WaitForSeconds(_delayToPool);
            EnemyManager.Instance.Pool(enemyType, this);
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
        void AttackState()
        {
            if (attack_SO)
            {
                _enemyAgent.isStopped = true;
                Vector3 dir = (_objective.position - transform.position).normalized;

                transform.forward = dir;
                Vector3 rot = transform.eulerAngles;
                rot.x = 0;
                rot.z = 0;
                transform.eulerAngles = rot;

                attack_SO.Refresh(Animator);
            }
        }

        void GetReadyToAttack()
        {
            if (Vector3.Distance(transform.position, _objective.transform.position) <= attackRange)
                enemyState = EnemyStates.Attacking;
            else
            {
                _enemyAgent.isStopped = false;
                enemyState = EnemyStates.Wander;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            #region Deal damage to Nexus
            if (!collision.gameObject.CompareTag("Nexus")) return;
            GotShot(currentHp);
            _delayToPool = 0;
            NexusManager.Instance.DealDamage(EnemyDamageToNexus);
            #endregion
        }
        #endregion

        #region Old targeting system
        /*public Transform[] targets;
        int waypointCounter = 0;
        void WaypointsCheck()
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
        #endregion
    }
}