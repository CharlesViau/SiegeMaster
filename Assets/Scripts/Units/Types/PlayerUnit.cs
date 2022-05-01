using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerUnit : Unit, ICameraController
    {
        private Rigidbody _rigidbody;

        public override void Init()
        {
            base.Init();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(transform.position + direction * (speed * Time.fixedDeltaTime));
        }

        public void Look(float cameraYAxis)
        {
            //transform.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.Euler(0, cameraYAxis, 0), turningSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0, cameraYAxis, 0);
        }
    }
}