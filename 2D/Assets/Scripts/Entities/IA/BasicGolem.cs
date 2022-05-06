using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGolem : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private float health;
    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDie();
        Move();
    }

    private void Move()
    {

    }

    private void CheckDie()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

}
