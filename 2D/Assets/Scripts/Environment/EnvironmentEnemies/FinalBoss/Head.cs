using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Animator headAnimator;
    public Animator leftCheekAnimator;
    public Animator rightCheekAnimator;

    public void Lower()
    {
        headAnimator.SetTrigger("lower");
    }

    public void Rise()
    {
        headAnimator.SetTrigger("rise");
    }

    public void FallLeft()
    {
        leftCheekAnimator.SetTrigger("fall");
    }

    public void FallRight()
    {
        rightCheekAnimator.SetTrigger("fall");
    }
}
