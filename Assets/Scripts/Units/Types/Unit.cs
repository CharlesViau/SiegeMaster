using Abilities;
using General;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Animator), typeof(AbilityHandler))]
    public abstract class Unit : MonoBehaviour, IUpdatable, IPoolable, IMovable
    {
        #region Properties and Variables

        //Component Cache
        protected Animator Animator;
        public AbilityHandler AbilityHandler { get; private set; }

        //Variables
        [SerializeField] public float turningSpeed;
        private float _turnSmoothVelocity;
        [SerializeField] public float speed;

        #endregion

        public virtual void Init()
        {
            //Caching Components
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

        public virtual void Move(Vector3 direction) { }
    }
}