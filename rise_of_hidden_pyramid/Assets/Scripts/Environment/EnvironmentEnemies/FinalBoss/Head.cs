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
        headAnimator.SetTrigger("fall");
        Debug.Log("Està caient");
        StartCoroutine(WaitToEndGame());
    }

    private IEnumerator WaitToEndGame()
    {
        yield return new WaitForSeconds(fallEndGameDelay);
        Debug.Log("Ara se'n va");
        finalBoss.EndOfGame();
    }
}
