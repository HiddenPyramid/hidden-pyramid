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
    private void Start()
    {
        initialHP = Health;
    }
    // Update is called once per frame
    void Update()
    {
        playersDetected = detection.PlayersDetected;
        CheckHealth();
        CheckDie();
        Move();
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
        transform.position += transform.right * Speed * Time.deltaTime;
    }

    private void Chase()
    {
        Vector2 dir = new Vector2(playersDetected[0].position.x - transform.position.x, 0);
        transform.Translate(dir * SprintSpeed * Time.deltaTime);
    }
    private void CheckHealth()
    {
        if (Health / initialHP < 0.5 || Health / initialHP < 0.25 && Arms.Count > 0)
            Arms[0].GetComponent<ArmFall>().Drop();
    }

}
