using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public class PlayerUnit : Unit, ICameraController
    {
        private CharacterController _characterController;
        private PlayerController _playerController;

        private const float Gravity = -20f;
        public float jumpHeight;
        
        private Vector3 _moveVelocity;
        private Vector3 _velocity;

        public float groundDistance = 0.4f;
        public LayerMask groundLayer;
        public Transform groundCheck;
        private bool _isGrounded;

        protected override Vector3 targetPosition => _playerController.HitPoint;

        public override void Init()
        {
            base.Init();
            _characterController = GetComponent<CharacterController>();
            _playerController = GetComponent<PlayerController>();

        }
        public override void Refresh()
        {
            base.Refresh();
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            if (_isGrounded && _velocity.y<0)
            {
                _velocity.y = -2f;
            }
            
        }
        public override void FixedRefresh()
        {
            _velocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
        public override void Move(Vector3 direction)
        {
            _moveVelocity = (direction) * Time.fixedDeltaTime * speed;
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
               _velocity.y = Mathf.Sqrt(jumpHeight*-2f * Gravity);
            }
           
        }
        public void Look(float cameraYAxis)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraYAxis, 0), turningSpeed * Time.fixedDeltaTime);

        }
    }
}