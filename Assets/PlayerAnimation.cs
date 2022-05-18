using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController=GetComponent<CharacterController>();    
    }
    public void Update()
    {
      //  Debug.Log(characterController.velocity.magnitude);
        //animator.SetFloat("Speed", 7);
         
    }
}
