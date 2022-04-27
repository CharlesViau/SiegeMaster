using Units.Interfaces;
using Units.Statistics;
using UnityEngine;

namespace Units.Types
{
    public class PlayerUnit : Unit, ICameraController
    {
        public override void FixedRefresh()
        {
        }

        public override void Move(Vector3 direction)
        {
            Rigidbody.MovePosition(transform.position + direction * (speed * Time.fixedDeltaTime));
        }

        public void Look(float cameraYAxis)
        {
            //transform.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.Euler(0, cameraYAxis, 0), turningSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0, cameraYAxis, 0);
        }
    }
}