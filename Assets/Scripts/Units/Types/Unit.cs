using General;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Animator))]
    public abstract class Unit : MonoBehaviour, IUpdatable, IPoolable, IMovable
    {
        #region Properties and Variables

        //Component Cache
        protected Rigidbody Rigidbody;
        protected Animator Animator;

        //Variables
        [SerializeField] public float turningSpeed;
        private float _turnSmoothVelocity;
        [SerializeField] public float speed;

        #endregion

        public virtual void Init()
        {
            //Caching Components
            TryGetComponent<Rigidbody>(out Rigidbody);
            Animator = GetComponent<Animator>();
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
    }
}