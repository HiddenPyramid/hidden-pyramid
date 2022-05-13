using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private float push;
    private InputAction jumpAction;
    private PlayerStats player;
    private CollisionManager collision;
    private Rigidbody rigidbody;

    private ChAnimation chAnimation;
    public Animator [] hearts;
    private float gainHeartDelay = 0.5f;


    private void Start()
    {
        player = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        collision = GetComponent<CollisionManager>();
        jumpAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_JUMP];
        chAnimation = GetComponent<ChAnimation>();

        jumpAction.performed += _ => Jump();
        jumpAction.canceled += _ => Fall();

    }
    private void FixedUpdate()
    {
        SetGravity();   
        CheckEndJump();
    }
    private void SetGravity()
    {
        if (collision.InWall)
        {
            rigidbody.velocity = new Vector3(0f, player.GravityValue * Time.deltaTime, rigidbody.velocity.z);
        }
        else if (collision.InGround && rigidbody.velocity.y < 0)
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        else
            rigidbody.velocity += new Vector3(0f, player.GravityValue * Time.deltaTime, 0f);
    }

    private void Jump()
    {
        if (collision.InGround)
        {
            Vector3 vec = new Vector3(rigidbody.velocity.x, player.JumpHeight, rigidbody.velocity.z);
            rigidbody.velocity = vec;
            chAnimation.StartJump();
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
        try
        {
            if (dmg <= 1){
                player.Health -= 1;
                hearts[player.Health].SetTrigger("lost");
            }
            else{
                player.Health -= 2;
                hearts[player.Health+1].SetTrigger("lost");
                hearts[player.Health].SetTrigger("lost");
            }
        } catch {}
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