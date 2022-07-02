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
    public float fallEndGameDelay = 20f;

    private bool isLowered = false;

    public void Lower()
    {
        headAnimator.SetTrigger("lower");
        isLowered = true;
    }

    public void Rise()
    {
        headAnimator.SetTrigger("rise");
        isLowered = false;
    }

    public void RiseIfLowered()
    {
        if (isLowered) Rise();
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
        rightAlive = false;
        if (!leftAlive) FallBoss();
    }

    public void FallBoss()
    {
        headAnimator.SetTrigger("fall");
        StartCoroutine(WaitToEndGame());
    }

    private IEnumerator WaitToEndGame()
    {
        yield return new WaitForSeconds(fallEndGameDelay);
        finalBoss.EndOfGame();
    }
}
