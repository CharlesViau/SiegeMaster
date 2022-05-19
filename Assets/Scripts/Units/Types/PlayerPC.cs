using Inputs;
using Units.Interfaces;
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
        Rigidbody rb;
        PlayerAnimation PlayerAnimation;

        protected override Vector3 AimedPosition => _cameraRayCast.RayCast(maxDistanceAiming, rayCastStartPointDistance);

        public override void Init()
        {
            base.Init();
            PlayerAnimation =GetComponent<PlayerAnimation>();
            rb =GetComponent<Rigidbody>();
             _cameraRayCast = FindObjectOfType<CameraRaycast>();

        }
        public override void Refresh()
        {
            base.Refresh();
            
         
            
        }
        public override void FixedRefresh()
        {
            
        }
        public override void Move(Vector3 direction)
        {
           
            rb.AddForce(direction* playerForce);
            if (rb.velocity.magnitude >maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }
        public void Rotate(Vector3 target)
        {
            transform.LookAt(target);

        }


        public override void Jump()
        {
            PlayerAnimation.Jump();
        }
        public override void Look()
        {
            playerRotationLook.forward = (AimedPosition - playerRotationLook.position).normalized;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, playerRotationLook.transform.eulerAngles.y, 0), turningSpeed*Time.fixedDeltaTime);

        }

    }
}