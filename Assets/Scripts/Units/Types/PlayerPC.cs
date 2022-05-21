using UnityEngine;

namespace Units.Types
{

    public class PlayerPC : PlayerUnit
    {
        private CameraRaycast _cameraRayCast;

        public Transform playerRotationLook;
        public float maxDistanceAiming;
        public float rayCastStartPointDistance;

        public float playerForce;
        public float maxSpeed;
        PlayerAnimation PlayerAnimation;

        public Vector3 hitpoint;
        protected override Vector3 AimedPosition
        {
            get
            {
                return hitpoint;
            }

        }

        public override void Init()
        {
            base.Init();
            PlayerAnimation = GetComponent<PlayerAnimation>();
            _cameraRayCast = FindObjectOfType<CameraRaycast>();

        }
        public override void Refresh()
        {
            base.Refresh();
            hitpoint = _cameraRayCast.RayCast(maxDistanceAiming, rayCastStartPointDistance);
        }
        public override void FixedRefresh()
        {
        }
        public override void Move(Vector3 direction)
        {

            Rigidbody.AddForce(direction * playerForce);
            if (Rigidbody.velocity.magnitude > maxSpeed)
            {
                Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, maxSpeed);
            }
        }
        public void Rotate(Vector3 target)
        {
            transform.LookAt(target);
        }


        public override void Jump()
        {
            if (PlayerAnimation.isGrounded)
            {
                PlayerAnimation.Jump();
                Rigidbody.AddForce(Vector3.up * 1000, ForceMode.Impulse);
            }


        }
        public override void Look()
        {
            playerRotationLook.forward = (AimedPosition - playerRotationLook.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, playerRotationLook.transform.eulerAngles.y, 0), turningSpeed * Time.fixedDeltaTime);

        }

    }
}