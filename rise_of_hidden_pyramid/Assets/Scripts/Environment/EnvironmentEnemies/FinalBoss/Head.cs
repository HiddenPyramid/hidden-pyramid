using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Animator headAnimator;
    public Animator leftCheekAnimator;
    public Animator rightCheekAnimator;
    public bool alive => leftAlive || rightAlive;
    public bool leftAlive = true;
    public bool rightAlive = true;

    public FinalBoss finalBoss;

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
        leftAlive = false;
        if (!rightAlive) FallBoss();
    }

    public void FallRight()
    {
        rightCheekAnimator.SetTrigger("fall");
        rightAlive = true;
        if (!leftAlive) FallBoss();
    }

    public void FallBoss()
    {
        Debug.Log("Head FallBoss");
        headAnimator.SetTrigger("fall");
        finalBoss.EndOfGame();
    }
}
