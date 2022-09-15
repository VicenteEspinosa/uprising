using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public int atackCooldownTime = 1;

    bool isAtacking = false;
    Animator animator;
    float endAtackCooldownTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal Firefighter");
        float atack = Input.GetAxis("Atack Firefighter");

        // animator.SetInteger("InputX", horizontal);
        if (horizontal > 0)
        {
            animator.SetBool("MovingRight", true);
            animator.SetBool("MovingLeft", false);
        }
        else if (horizontal < 0)
        {
            animator.SetBool("MovingRight", false);
            animator.SetBool("MovingLeft", true);
        }
        else
        {
            animator.SetBool("MovingRight", false);
            animator.SetBool("MovingLeft", false);
        }

        if (Time.time > endAtackCooldownTime)
        {
            if (isAtacking)
            {
                animator.ResetTrigger("Atack");
                isAtacking = false;
            }
            if (atack > 0)
            {
                animator.SetTrigger("Atack");
                isAtacking = true;
                endAtackCooldownTime = Time.time + atackCooldownTime;
            }
        }
    }
}
