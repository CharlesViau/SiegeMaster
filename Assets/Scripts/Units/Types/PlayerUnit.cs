using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public abstract class PlayerUnit : Unit,ICameraController,IHittable
    {
        protected abstract override Vector3 AimedPosition { get; }
        public void GotShot(float damage)
        {
            Debug.Log(damage +"to player");
        }

        public abstract void Look();

    }
}