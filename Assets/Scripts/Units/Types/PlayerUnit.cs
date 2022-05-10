using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerUnit : Unit, ICameraController
    {
        private Rigidbody _rigidbody;
        private PlayerController _controller;


        protected override Vector3 targetPosition => _controller.HitPoint;

        public override void Init()
        {
            base.Init();
            _rigidbody = GetComponent<Rigidbody>();
            _controller = GetComponent<PlayerController>();
        }

        public override void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(transform.position + direction * (speed * Time.fixedDeltaTime));
        }

        public void Look(float cameraYAxis)
        {
            transform.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.Euler(0, cameraYAxis, 0), turningSpeed * Time.fixedDeltaTime);
        }
    }
}