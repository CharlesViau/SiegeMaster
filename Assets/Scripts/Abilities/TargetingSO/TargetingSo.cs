using Units.Interfaces;
using UnityEngine;

namespace Abilities.TargetingSO
{
    public abstract class TargetingSo : ScriptableObject
    {
        protected ITargetAcquirer Owner;
        protected float MaxRange;
        public Transform TargetTransform { get; set; }

        public virtual void Init(ITargetAcquirer owner, float maxRange)
        {
            this.Owner = owner;
            MaxRange = maxRange;
        }

        public abstract void Refresh();
    }
}