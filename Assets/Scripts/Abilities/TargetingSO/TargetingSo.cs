using Units.Interfaces;
using UnityEngine;

namespace Abilities.TargetingSO
{
    public abstract class TargetingSo : ScriptableObject
    {
        protected ITargetAcquirer Owner;
        protected float MaxRange;
        
        public abstract Transform TargetTransform { get; }
        
        public virtual void Init(ITargetAcquirer owner, float maxRange)
        {
            Owner = owner;
            MaxRange = maxRange;
        }

        public abstract void Refresh();
    }
}