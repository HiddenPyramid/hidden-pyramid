using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public Animator animator;

    public void Lower()
    {
        animator.SetTrigger("lower");
    }

    public void Rise()
    {
        animator.SetTrigger("rise");
    }

    public void Fall()
    {
        animator.SetTrigger("fall");
    }
}
