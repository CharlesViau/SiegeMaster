using General;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public abstract class Unit : MonoBehaviour, IUpdatable, IPoolable, IMovable
    {
        #region Properties and Variables

        //Component Cache
        private Rigidbody _rigidbody;
        private Animator _animator;

        //Variables
        [SerializeField] public float turningSpeed;
        private float _turnSmoothVelocity;
        [SerializeField] public float speed;

        #endregion

        public virtual void Init()
        {
            //Caching Components
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
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
            var smoothAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg,
                    ref _turnSmoothVelocity, turningSpeed);

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            _rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }
}