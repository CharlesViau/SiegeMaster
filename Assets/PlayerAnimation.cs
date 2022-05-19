using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Units.Types;

public class PlayerAnimation : MonoBehaviour
{

    Animator animator;
    PlayerPC playerPC;
    Rigidbody rb;
    private void Start()
    {
        playerPC = GetComponent<PlayerPC>();    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();    
    }
    public void Update()
    {
        float anglForward = Vector3.Angle(transform.forward,rb.velocity);
        float anglLeftRight = Vector3.Angle(transform.right,rb.velocity);
        animator.SetFloat("SP", Mathf.Cos(anglLeftRight * Mathf.Deg2Rad ) *rb.velocity.magnitude / playerPC.maxSpeed);
        animator.SetFloat("SpeedZ",Mathf.Cos(anglForward * Mathf.Deg2Rad) *rb.velocity.magnitude / playerPC.maxSpeed);
         
    }
}
