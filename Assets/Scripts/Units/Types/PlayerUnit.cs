using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public abstract class PlayerUnit : Unit,ICameraController,IHittable
    {
        protected override Vector3 targetPosition => throw new System.NotImplementedException();

        public void GotShot(float damage)
        {
            Debug.Log(damage +"to player");
        }

        public abstract void Look();

    }
}