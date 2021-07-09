using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public bool isGrounded;
    public Vector3 velocity;
    public Animator animator;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGrounded", isGrounded);
        if (Mathf.Abs(velocity.x) > 0 || Mathf.Abs(velocity.z) > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else 
        {
            animator.SetBool("isRunning", false);
        }
    }
    public void JumpStart()
    {
        animator.SetTrigger("JumpStart");
    }
    public void FreefallStart()
    {
        animator.SetTrigger("FreefallStart");
       // isFalling = true;
    }
    public void JumpEnd()
    {
        //isFalling = false;
        animator.SetTrigger("JumpEnd");
    }
}
