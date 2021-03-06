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
    [SerializeField]
    protected Animator shadowAnimator;

    public ParticleSystem dieParticles;
    public ParticleSystem[] attackParticles;

    protected List<Transform> playersDetected;
    protected PlayerDetection detection;
    protected GolemCollision collision;

    

    private float distToPlayer;
    private bool attacking;

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
    [SerializeField] private float timeBetwenExplosions = .1f;
    private float initialHP;
    private bool dead = false;
    private int armIndex;
    private PlayerManager playerManager;
    private bool playerAlive = true;

    public enum DirectionSegment
    {
        first, second, third
    }

    public DirectionSegment directionSegment;

    void Awake()
    {
        detection = GetComponent<PlayerDetection>();
        collision = GetComponent<GolemCollision>();
    }

 
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        mustPatrol = true;
        player = playerManager.GetPlayer().gameObject.transform;
        playerManager.playerChangeEvent += GetCurrentPlayer;

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
            shadowAnimator.SetBool("isInRange", false);
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
                if (playerManager.GetHealth() <= 0) {
                    animator.SetBool("isInRange", false);
                    shadowAnimator.SetBool("isInRange", false);
                } else {
                    mustPatrol = false;
                    AttackPlayer();
                }
            }
        } 
        else
        {
            animator.SetBool("isInRange", false);
            shadowAnimator.SetBool("isInRange", false);
            mustPatrol = true;
        }
    }
    

    private void AttackPlayer()
    {
        attacking = true;
        animator.SetBool("punching", true);
        animator.SetBool("isInRange", true);
        shadowAnimator.SetBool("punching", true);
        shadowAnimator.SetBool("isInRange", true);
        StartCoroutine(attackParticleAnimation());
    }

    IEnumerator attackParticleAnimation()
    {
        yield return new WaitForSeconds(1.27f);
        for (int i = 0; i<attackParticles.Length; i++)
        {
            attackParticles[i].Play();
            yield return new WaitForSeconds(timeBetwenExplosions);
        }
        animator.SetBool("punching", false);
        shadowAnimator.SetBool("punching", false);
        attacking = false;
    }
    protected void Move()
    {
        if (!attacking)
        {
            if (mustFlip)
            {
                Flip();
            }
            switch (directionSegment)
            {
                case DirectionSegment.first:
                    MoveX();
                    break;
                case DirectionSegment.second:
                    MoveZ();
                    break;
                case DirectionSegment.third:
                    MoveNegativeX();
                    break;
            }    
        }
        
    }

    private void MoveX()
    {
        rb.velocity = new Vector3(Speed * Time.fixedDeltaTime * -1 , rb.velocity.y, rb.velocity.z);
    }

    private void MoveZ()
    {
        rb.velocity = new Vector3(rb.velocity.x , rb.velocity.y, Speed * Time.fixedDeltaTime * -1);
    }

    private void MoveNegativeX()
    {
        rb.velocity = new Vector3(- Speed * Time.fixedDeltaTime * -1 , rb.velocity.y, rb.velocity.z);
    }

    public void Flip()
    {
        mustPatrol = false;
        for (int i = 0; i < attackParticles.Length; i++)
        {
            attackParticles[i].Stop();
        }
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
            animator.SetBool("hasDied", true);
            shadowAnimator.SetBool("hasDied", true);
            DeactivateColliders();
            Instantiate(dieParticles, transform.position, transform.rotation, null);
            dead = true;
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
        }
        if (RemainingArms() && FallingArmThresholdPassed())
        {
            animator.SetTrigger("tookDamage");
            shadowAnimator.SetTrigger("tookDamage");
            ArmVisuals[armIndex].gameObject.SetActive(false);
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
        gameObject.GetComponent<Rigidbody>().useGravity = false;
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
        Health -= dmg;
    }
}
