using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Units.Types;

public enum PlayerStateAnimation { Falling,Running ,landed }
public class PlayerAnimation : MonoBehaviour
{
    public Transform groundCheckPosition;
    PlayerStateAnimation  playerStateAnimation= new PlayerStateAnimation();
    Animator animator;
    PlayerPC playerPC;
    Rigidbody rb;
    bool canLand;
    bool isGrounded;
    private void Start()
    {
        playerPC = GetComponent<PlayerPC>();    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerStateAnimation = PlayerStateAnimation.Running;
    }
    public void Update()
    {
        
        if (Physics.Raycast(groundCheckPosition.position, -Vector3.up, 0.1f) == false )
        {
            playerStateAnimation = PlayerStateAnimation.Falling;
            canLand = true;
        }
        if(Physics.Raycast(groundCheckPosition.position, -Vector3.up, 0.1f) && canLand)
        {
            playerStateAnimation = PlayerStateAnimation.landed;
            canLand = false;
        }
        

        switch (playerStateAnimation)
        {
            case PlayerStateAnimation.Falling:
                {

                    animator.SetBool("Flying", true);
                    break;
                }

               
            case PlayerStateAnimation.landed:
                {
                    animator.SetBool("Flying", false);
                    animator.SetTrigger("Land");
                    playerStateAnimation = PlayerStateAnimation.Running;

                    break;
                }
            case PlayerStateAnimation.Running:
                {
                    float anglForward = Vector3.Angle(transform.forward, rb.velocity);
                    float anglLeftRight = Vector3.Angle(transform.right, rb.velocity);
                    animator.SetFloat("SP", Mathf.Cos(anglLeftRight * Mathf.Deg2Rad) * rb.velocity.magnitude / playerPC.maxSpeed);
                    animator.SetFloat("SpeedZ", Mathf.Cos(anglForward * Mathf.Deg2Rad) * rb.velocity.magnitude / playerPC.maxSpeed);
                    break;
                }               
            default:
                break;
        }
        //.Log(grandCheckDistance);        
    }
    public void Jump()
    {
        animator.SetTrigger("Jump");
    }
    
}
