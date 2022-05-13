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
    private List<Transform> Arms;

    private float initialHP;
    private Vector3 lastPos;
    private bool dead = false;
    private void Start()
    {
        initialHP = Health;
        lastPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            playersDetected = detection.PlayersDetected;
            CheckHealth();
            if (collision.Collided == null)
            {
                CheckAll();
                Move();
            }
            else
                Attack();
        }

    }

    private void Attack()
    {
        animator.SetTrigger("punch");
        GetComponent<DamageDealer>().DealDamage(collision.Collided);
    }

    private void CheckAll()
    {
        CheckCollision();
        CheckDirection();
    }

    private void CheckCollision()
    {
        if (!collision.InGround)
            Speed *= -1;
    }

    private void CheckDirection()
    {
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
        else if (Arms.Count > 0 && Health / initialHP < 0.5 || Health / initialHP < 0.25)
            Arms[0].GetComponent<ArmFall>().Drop();
    }

}
