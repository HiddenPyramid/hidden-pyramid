using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGolem : Golem
{
    [SerializeField]
    private float SprintSpeed;
    [SerializeField]
     private float deathDelay;
    [SerializeField]
    private List<Transform> ArmVisuals;
    [SerializeField]
    private List<ArmFall> ArmRagdolls;

    private float initialHP;
    private Vector3 lastPos;
    private bool dead = false;

    private int armIndex;
    private void Start()
    {
        initialHP = Health;
        lastPos = transform.position;
        armIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            playersDetected = detection.PlayersDetected;
            CheckAll();
            Move();
        }

    }


    private void CheckAll()
    {
        CheckHealth();
        CheckDirection();
    }


    private void CheckDirection()
    {
        if (!collision.InGround)
            Speed *= -1;
        var dir = lastPos - Vector3.Scale(transform.position, transform.right);
        var scale = (dir.x < 0 || dir.z < 0) ? -1 : 1;
        Visuals.localScale = new Vector3(scale, Visuals.localScale.y, Visuals.localScale.z);
        lastPos = transform.position;
    }

    protected override void Move()
    {
        if (playersDetected.Count > 0)
        {
            Chase();
            animator.SetBool("chase", true);
        }
        else
        {
            Patrol();
            animator.SetBool("chase", false);
        }
    }

    private void Patrol()
    {
        transform.position += Speed * Time.deltaTime * transform.right;
    }

    private void Chase()
    {
        Vector3 dir = playersDetected[0].position - transform.position;
        dir.Scale(transform.right);
        transform.position += SprintSpeed * Time.deltaTime * dir.normalized;
    }
    private void CheckHealth()
    {
        if (CheckDie())
        {
            animator.SetTrigger("die");
            dead = true;
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
        }
        if (RemainingArms() && FallingArmThresholdPassed())
        {
            ArmVisuals[armIndex].gameObject.SetActive(false);
            Debug.Log("Ei desactivat");
            Instantiate(ArmRagdolls[armIndex], ArmVisuals[armIndex].position, ArmVisuals[armIndex].rotation);
            armIndex = armIndex+1;
        }
    }

    private bool RemainingArms()
    {
        return this.armIndex < this.ArmVisuals.Count;
    }

    private bool FallingArmThresholdPassed()
    {
        float inverseIndex = this.ArmVisuals.Count - this.armIndex - 1;
        float armThreshold = 1.0f / (float)this.ArmVisuals.Count * inverseIndex; 
        return (Health / initialHP) <= (armThreshold);
    }
}
