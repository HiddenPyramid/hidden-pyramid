using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    private InputAction jumpAction;
    private PlayerStats player;
    private CollisionManager collision;
    private Rigidbody rigidbody;
    public SoundManager soundManager;

    private ChAnimation chAnimation;
    public Animator [] hearts;
    public Animator plAnimator;
    private float gainHeartDelay = 0.5f;

    public float invulnerabilityTime = 3f;
    private bool canTakeDamage = true;

    private PlayerMovement playerMovement;
    public CameraController cameraController;
    public Animator curtainAnimator;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        collision = GetComponent<CollisionManager>();
        jumpAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_JUMP];
        chAnimation = GetComponent<ChAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        
        jumpAction.performed += _ => Jump();
        jumpAction.canceled += _ => Fall();

    }
    private void FixedUpdate()
    {
        SetGravity();   
        CheckEndJump();
        CheckWalled();

        Debug.Log(PlayerStats.Health);
    }

    private void CheckWalled()
    {
    }

    private void SetGravity()
    {
        if (collision.InWall)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, Math.Max(rigidbody.velocity.y, -5), rigidbody.velocity.z);
        }
        else if (collision.InGround && rigidbody.velocity.y < 0)
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        else
            rigidbody.velocity += new Vector3(0f, PlayerStats.GravityValue * Time.deltaTime, 0f);
    }

    private void Jump()
    {
        if (collision.InGround  && !playerMovement.moveBlocked)
        {
            Vector3 vec = new Vector3(rigidbody.velocity.x, player.JumpHeight, rigidbody.velocity.z);
            rigidbody.velocity = vec;
            chAnimation.StartJump();
            soundManager.audioSelect(0, 0.5f);
        }
    }


    private void Fall()
    {
        if (!collision.InGround)
        {
            //EventManager.Landing();
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        }
    }

    private void CheckEndJump()
    {
        if (collision.InGround)
            chAnimation.EndJump();
        else
            chAnimation.StartJump();
    }

    public void TakeDamage(float dmg)
    {
        if (canTakeDamage)
        {
            StartCoroutine(Invulnerability());
            try
            {
                if (dmg <= 1){
                    Debug.Log(PlayerStats.Health+" la saluuuut");
                    curtainAnimator.SetTrigger("takeDamage");
                    cameraController.cameraShaking = true;
                    plAnimator.SetTrigger("tookDamage");
                    PlayerStats.Health -= 1;
                    hearts[PlayerStats.Health].SetTrigger("lost");
                }
                else{
                    Debug.Log("Damage");
                    curtainAnimator.SetTrigger("takeDamage");
                    cameraController.cameraShaking = true;
                    plAnimator.SetTrigger("tookDamage");
                    PlayerStats.Health -= 2;
                    hearts[PlayerStats.Health+1].SetTrigger("lost");
                    hearts[PlayerStats.Health].SetTrigger("lost");
                }
            } catch {}
        }
    }

    private IEnumerator Invulnerability()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityTime);
        canTakeDamage = true;
    }

    public void RegainLives()
    {
        StartCoroutine(GainLives());
    }
    
    private IEnumerator GainLives()
    {
        yield return new WaitForSeconds(gainHeartDelay);
        foreach (Animator heart in hearts)
        {
            heart.SetTrigger("gained");
            yield return new WaitForSeconds(gainHeartDelay);
        }
    }
}