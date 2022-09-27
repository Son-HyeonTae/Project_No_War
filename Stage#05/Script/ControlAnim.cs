using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnim : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GMScene5.isStart)
        {
            animator.SetBool("isStart", true);
        }
        if(GMScene5.isGameover)
        {
            animator.SetBool("isStart", false);
        }
    }
}
