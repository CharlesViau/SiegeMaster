using Units.Interfaces;
using Units.Statistics;
using UnityEngine;

namespace Units.Types
{

    public abstract class PlayerUnit : Unit,ICameraController,IHittable
    {
        protected abstract override Vector3 AimedPosition { get; }

        public Health health;
        public Mana mana;
        public void GotShot(float damage)
        {
            health.Current -= damage;
        }

        public abstract void Look();

    }
}