using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour
{
    public CharacterMovement CharacterMovementComponent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimControl();
        JumpAnimControl();
    }

    void MoveAnimControl()
    {
        Vector3 CharacterAxis = CharacterMovementComponent.GetCharacterAxis();

        animator.SetBool("Acceleration", false);
        if (CharacterAxis != Vector3.zero)
        {
            animator.SetBool("Acceleration", true);
            if (CharacterMovementComponent.IsRunning)
            {
                animator.SetBool("PressedRunKey", true);
            }
            else if (CharacterMovementComponent.IsWalking)
            {
                animator.SetBool("PressedRunKey", false);
            }
        }

    }


    void JumpAnimControl()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJump");
        }

        if(CharacterMovementComponent.bCheckCharacterOntheGround == false)
        {
            animator.SetBool("isFloating", true);
        }
        else if(CharacterMovementComponent.bCheckCharacterOntheGround == true)
        {
            animator.SetBool("isFloating", false);
        }
    }
}
