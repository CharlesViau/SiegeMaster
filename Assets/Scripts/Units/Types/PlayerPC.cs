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
        public float jumpHeight;
      
        private CharacterController _characterController;
        
        public float groundDistance = 0.4f;
        public LayerMask groundLayer;
        public Transform groundCheck;


        private Vector3 _moveVelocity;
        private Vector3 _gravityVelocity;

        private bool _isGrounded;
        protected override Vector3 AimedPosition => _cameraRayCast.RayCast(maxDistanceAiming, rayCastStartPointDistance);
        private const float Gravity = -20f;

        public override void Init()
        {
            base.Init();
            _cameraRayCast = FindObjectOfType<CameraRaycast>();
            _characterController = GetComponent<CharacterController>();

        }
        public override void Refresh()
        {
            base.Refresh();
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            if (_isGrounded && _gravityVelocity.y<0)
            {
                _gravityVelocity.y = -2f;
            }
            
        }
        public override void FixedRefresh()
        {
            _gravityVelocity.y += Gravity * Time.fixedDeltaTime;
            _characterController.Move((_gravityVelocity) * Time.fixedDeltaTime);
        }
        public override void Move(Vector3 direction)
        {
            _moveVelocity = (direction) * Time.fixedDeltaTime * speed;
         //  Animator.SetFloat(_moveVelocity)
            _characterController.Move(_moveVelocity);
            _moveVelocity.y = 0;
        }
        public void Rotate(Vector3 target)
        {
            transform.LookAt(target);

        }


        public override void Jump()
        {
            if (_isGrounded)
            {
               _gravityVelocity.y = Mathf.Sqrt(jumpHeight*-2f * Gravity);
            }
        }
        public override void Look()
        {
            playerRotationLook.forward = (AimedPosition - playerRotationLook.position).normalized;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, playerRotationLook.transform.eulerAngles.y, 0), turningSpeed*Time.fixedDeltaTime);

        }

    }
}