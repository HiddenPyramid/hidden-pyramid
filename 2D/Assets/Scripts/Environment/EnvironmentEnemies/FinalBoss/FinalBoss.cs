using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Arm leftArm;
    public Arm rightArm;
    public Head headWithBody;

    public float initialDownTime = 15f;
    public float finalDownTime = 5f;
    public float initialDelay = 20f;
    public float finalDelay = 5f;
    public float downTime, delay;
    public float decrement = 0.5f;

    public bool leftReady = true;
    public bool rightReady = true;
    public bool headReady = true;
    public float checkInterval = 1f;

    private void Start() 
    {
        downTime = initialDownTime;
        delay = initialDelay;
    }
    private IEnumerator BossAttack()
    {
        while (rightArm.alive || leftArm.alive)
        {
            while (!this.rightReady) { yield return new WaitForSeconds(checkInterval); }
            if (rightArm.alive) {
                StartCoroutine(RightAttack());
                yield return new WaitForSeconds(delay);
            }

            while (!this.leftReady) { yield return new WaitForSeconds(checkInterval); }
            if (leftArm.alive) {
                StartCoroutine(LeftAttack());
            }
            
            DecrementDelay();
        }
        while (headWithBody.alive)
        {
            StartCoroutine(HeadAttack());
            yield return new WaitForSeconds(delay);
            DecrementDelay();
        }
    }

    private IEnumerator RightAttack()
    {
        this.rightReady = false;
        this.rightArm.Lower();
        Debug.Log("RIGHT DOWN");
        yield return new WaitForSeconds(downTime);
        this.rightArm.Rise();
        DecrementDownTime();
        Debug.Log("RIGHT UP");

        this.rightReady = true;
    }

    private IEnumerator LeftAttack()
    {
        this.leftReady = false;

        this.leftArm.Lower();
        Debug.Log("LEFT DOWN");
        yield return new WaitForSeconds(downTime);
        this.leftArm.Rise();
        DecrementDownTime();
        Debug.Log("LEFT UP");

        this.leftReady = true;
    }

    private IEnumerator HeadAttack()
    {
        this.headReady = false;

        Debug.Log("HEAD DOWN");
        this.headWithBody.Lower();
        yield return new WaitForSeconds(downTime);
        this.headWithBody.Rise();
        DecrementDownTime();
        Debug.Log("HEAD UP");

        this.headReady = true;
    }

    public void Activate()
    {
        StartCoroutine(BossAttack());
    }

    private void DecrementDelay()
    {
        float decrementedValue = this.delay - this.decrement;
        this.delay = decrementedValue < this.finalDelay ? this.finalDelay : decrementedValue;
    }

    private void DecrementDownTime()
    {
        float decrementedValue = this.downTime - this.decrement;
        this.downTime = decrementedValue < this.finalDownTime ? this.finalDownTime : decrementedValue;
    }
}
