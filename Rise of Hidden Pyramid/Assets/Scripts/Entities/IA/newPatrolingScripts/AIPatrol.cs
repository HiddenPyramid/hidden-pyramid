using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol :  Golem
{
    private float distToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    public Rigidbody rb;
    public Transform wallCheckPos, player;
    public LayerMask wallLayer;
    public float range;

 
    void Start()
    {
        mustPatrol = true;
        player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
        FindObjectOfType<PlayerManager>().playerChangeEvent += GetCurrentPlayer;
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
            animator.SetBool("isInRange", false);
            Move();
        }
        distToPlayer = Vector3.Distance(transform.position, player.position);
        
        if(distToPlayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x > 0
            || player.position.x < transform.position.x && transform.localScale.x < 0)
                Flip();

            mustPatrol = false;
            AttackPlayer();
        } 
        else
        {
            animator.SetBool("isInRange", false);
            mustPatrol = true;
        }
    }
    
    private void AttackPlayer()
    { 
        animator.SetBool("isInRange", true);
    }

    protected override void Move()
    {
        
        if (mustFlip)
        {
            Flip();
        }
        rb.velocity = new Vector3(Speed * Time.fixedDeltaTime * -1 , rb.velocity.y, rb.velocity.z);
    }

    public void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        Speed *= -1;
        mustPatrol = true;
    }

    private void GetCurrentPlayer()
    {
        player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
    }
}
