using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGolem : Golem
{
    [SerializeField]
    private float SprintSpeed;
    [SerializeField]
    private List<Transform> Arms;

    private float initialHP;
    private Vector3 lastPos;
    private void Start()
    {
        initialHP = Health;
        lastPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        playersDetected = detection.PlayersDetected;
        if (!collision.Collided)
        {
            CheckAll();
            Move();
        }
        else
            Attack();

    }

    private void Attack()
    {
        
    }

    private void CheckAll()
    {
        CheckCollision();
        CheckHealth();
        CheckDirection();
    }

    private void CheckCollision()
    {
        if (!collision.InGround || collision.InWall)
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
        }
        else
            Patrol();
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
            Destroy(gameObject);
        else if (Health / initialHP < 0.5 || Health / initialHP < 0.25 && Arms.Count > 0)
            Arms[0].GetComponent<ArmFall>().Drop();
    }

}
