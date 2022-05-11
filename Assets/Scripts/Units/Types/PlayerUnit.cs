using Inputs;
using Units.Interfaces;
using UnityEngine;

namespace Units.Types
{

    public class PlayerUnit : Unit, ICameraController
    {
        private CharacterController characterController;

        private const float gravity = -20f;
        public float hightJump;
        
        Vector3 moveVlocity;
        Vector3 vlo;

        public float groundDistance = 0.4f;
        public LayerMask groundLayer;
        public Transform groundCheck;
        bool isGrounded;

        protected override Vector3 targetPosition => throw new System.NotImplementedException();

        public override void Init()
        {
            base.Init();
            characterController = GetComponent<CharacterController>();

        }
        public override void Refresh()
        {
            base.Refresh();
            //Collider[] sd = Physics.OverlapSphere(groundCheck.position, groundDistance, groundLayer);
            //for (int i = 0; i < sd.Length; i++)
            //{
            //    Debug.Log(sd[i].name);
            //}
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            if (isGrounded && vlo.y<0)
            {
                vlo.y = -2f;
            }
            
        }
        public override void FixedRefresh()
        {
            vlo.y += gravity * Time.deltaTime;
            characterController.Move(vlo * Time.fixedDeltaTime);
        }
        public override void Move(Vector3 direction)
        {
            moveVlocity = (direction) * Time.fixedDeltaTime * speed;
            characterController.Move(moveVlocity);
            moveVlocity.y = 0;
        }
        public void Rotate(Vector3 target)
        {
            transform.LookAt(target);

        }


        public override void Jump()
        {
            if (isGrounded)
            {
               vlo.y = Mathf.Sqrt(hightJump*-2f * gravity);
            }
           
        }
        public void Look(float cameraYAxis)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraYAxis, 0), turningSpeed * Time.fixedDeltaTime);

        }
    }
}