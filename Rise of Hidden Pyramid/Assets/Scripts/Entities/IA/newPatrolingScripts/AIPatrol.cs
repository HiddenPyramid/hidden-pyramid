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


    [SerializeField] private float deathDelay;
    [SerializeField] private List<Transform> ArmVisuals;
    [SerializeField] private List<ArmFall> ArmRagdolls;
    private float initialHP;
    private bool dead = false;
    private int armIndex;

    [SerializeField]
    protected float Health;

 
    void Start()
    {
        mustPatrol = true;
        player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
        FindObjectOfType<PlayerManager>().playerChangeEvent += GetCurrentPlayer;

        initialHP = Health;
        armIndex = 0;
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
        if (!dead)
        {
            CheckHealth();
            CheckDirection();
        }
    }

    private void CheckDirection()
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

    private void CheckHealth()
    {
        Debug.Log("EIII Healtsadasd "+Health);
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

    public override void TakeDamage(float dmg)
    {
        animator.SetTrigger("tookDamage");
        Debug.Log("Previous "+Health);
        Health -= dmg;
        Debug.Log("Current "+Health);
    }
    protected override bool CheckDie()
    {
        if (Health <= 0)
            return true;
        return false;
    }
}
