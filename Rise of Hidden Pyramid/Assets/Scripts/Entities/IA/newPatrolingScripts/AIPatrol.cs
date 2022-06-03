using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{


    [SerializeField]
    protected float Health;
    [SerializeField]
    protected float Speed;
    [SerializeField]
    protected bool Chase;
    [SerializeField]
    protected Transform Visuals;
    [SerializeField]
    protected Animator animator;

    protected List<Transform> playersDetected;
    protected PlayerDetection detection;
    protected GolemCollision collision;


    private float distToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    public Rigidbody rb;
    public Transform wallCheckPos, player;
    public LayerMask wallLayer;
    public float range = 8.0f;
    public float rangeToAttack = 3.0f;


    [SerializeField] private float deathDelay;
    [SerializeField] private List<Transform> ArmVisuals;
    [SerializeField] private List<ArmFall> ArmRagdolls;
    private float initialHP;
    private bool dead = false;
    private int armIndex;


    public ParticleSystem particleSystem;

    void Awake()
    {
        detection = GetComponent<PlayerDetection>();
        collision = GetComponent<GolemCollision>();
    }

 
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
            
            if (distToPlayer <= rangeToAttack)
            {
                mustPatrol = false;
                AttackPlayer();
            }
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

    protected void Move()
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
        if (CheckDie())
        {
            DeactivateColliders();
            Instantiate(particleSystem, transform.position, transform.rotation, null);
            animator.SetBool("hasDied", true);
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

    private void DeactivateColliders()
    {
        foreach (Collider collider in gameObject.GetComponents<Collider>())
        {
            collider.enabled = false;
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


    protected bool CheckDie()
    {
        if (Health <= 0){
            return true;
        }
        return false;
    }

    public void TakeDamage(float dmg)
    {
        animator.SetTrigger("tookDamage");
        Health -= dmg;
        Debug.Log(Health);
    }
}
