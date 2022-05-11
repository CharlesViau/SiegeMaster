using Abilities;
using General;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Animator), typeof(AbilityHandler))]
    public abstract class Unit : MonoBehaviour, IUpdatable, IPoolable, IMovable, ITargetAcquirer
    {
        #region Properties and Variables

        //Component Cache
        protected Rigidbody Rigidbody;
        protected Animator Animator;
        public AbilityHandler AbilityHandler { get; private set; }

        //Variables
        [SerializeField] public float turningSpeed;
        private float _turnSmoothVelocity;
        [SerializeField] public float speed;

        //Targeting
        [SerializeField] private Transform shootingPosition;
        public Transform ShootingPosition => shootingPosition;
        public Vector3 AimingDirection => TargetPosition - shootingPosition.position;

        protected abstract Vector3 targetPosition { get; }

        public Vector3 TargetPosition => targetPosition;
        public Transform Target { get; set; }

        #endregion

        public virtual void Init()
        {
            //Caching Components

            TryGetComponent<Rigidbody>(out Rigidbody);
            Animator = GetComponent<Animator>();
            AbilityHandler = GetComponent<AbilityHandler>();
        }

        public virtual void PostInit()
        {
        }

        public virtual void Refresh()
        {
        }

        public virtual void FixedRefresh()
        {
        }

        public void LateRefresh()
        {
            
        }

        public virtual void Pool()
        {
        }

        public virtual void Depool()
        {
        }
        
        public virtual void Move(Vector3 direction)
        {
            if (Rigidbody == null) return;
            var smoothAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg,
                    ref _turnSmoothVelocity, turningSpeed);

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }

        public virtual void Jump()
        {
        }
    }
}