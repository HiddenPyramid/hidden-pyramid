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

    public SceneLoadTrigger sceneLoadTrigger;

    [SerializeField] private bool playerHasDied = false;

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
            if (!playerHasDied)
            {
                while (!this.headReady) { yield return new WaitForSeconds(checkInterval); }
                if (!playerHasDied) StartCoroutine(HeadAttack());
                yield return new WaitForSeconds(delay);
                if (!playerHasDied) DecrementDelay();
            } else yield return new WaitForSeconds(checkInterval);
        }
        headWithBody.FallBoss();
        EndOfGame();
    }

    private IEnumerator RightAttack()
    {
        this.rightReady = false;
        this.rightArm.Lower();
        yield return new WaitForSeconds(downTime);
        this.rightArm.Rise();
        DecrementDownTime();

        this.rightReady = true;
    }

    private IEnumerator LeftAttack()
    {
        this.leftReady = false;

        this.leftArm.Lower();
        yield return new WaitForSeconds(downTime);
        this.leftArm.Rise();
        DecrementDownTime();

        this.leftReady = true;
    }

    private IEnumerator HeadAttack()
    {
        this.headReady = false;

        if (!playerHasDied) this.headWithBody.Lower();
        if (!playerHasDied) yield return new WaitForSeconds(downTime);
        if (!playerHasDied) this.headWithBody.Rise();
        if (!playerHasDied) DecrementDownTime();
        this.headReady = true;
    }

    public void ActivateFirstTime()
    {
        StartCoroutine(BossAttack());
    }

    public void Activate()
    {
        UnpauseBossAttack();
    }

    public void OnPlayerDie()
    {
        PauseBossAttack();
        StartCoroutine(RetireHead());
    }

    private IEnumerator RetireHead() { yield return new WaitForSeconds(2f); this.headWithBody.RiseIfLowered(); }
    private void PauseBossAttack() { this.playerHasDied = true;}
    private void UnpauseBossAttack() { this.playerHasDied = false;}

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

    public void EndOfGame()
    {
        GameManager.isCredits = true;
        sceneLoadTrigger.LoadNextScene();
    }

    public bool IsAlive()
    {
        Debug.Log("Left: "+headWithBody.leftAlive+" Right: "+headWithBody.rightAlive);
        return headWithBody.alive;
    }
}
