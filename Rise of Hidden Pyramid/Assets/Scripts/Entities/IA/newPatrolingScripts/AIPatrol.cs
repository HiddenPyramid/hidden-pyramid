using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol :  Golem
{   
    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    public Rigidbody rb;
    public Transform wallCheckPos;
    public LayerMask wallLayer;

    void Start()
    {
        mustPatrol = true;
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = Physics.CheckSphere(wallCheckPos.position, 0.1f, wallLayer);
        }
    }
    void Update()
    {
        if (mustPatrol)
        {
            Move();
        }
    }

    protected override void Move()
    {
      
        if (mustFlip)
        {
            Flip();
        }
        rb.velocity = new Vector3(Speed * Time.fixedDeltaTime * -1 , rb.velocity.y, rb.velocity.z);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        Speed *= -1;
        mustPatrol = true;
    }
}
