using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public abstract class PlayerUnit : Unit,ICameraController
    {
        protected override Vector3 targetPosition => throw new System.NotImplementedException();

        public abstract void Look();

    }
}