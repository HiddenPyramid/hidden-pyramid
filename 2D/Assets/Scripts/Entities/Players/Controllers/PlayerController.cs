using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    private InputAction jumpAction;
    private PlayerStats player;
    private CollisionManager collision;
    private Rigidbody rigidbody;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        collision = GetComponent<CollisionManager>();
        jumpAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_JUMP];

        jumpAction.performed += _ => Jump();
        jumpAction.canceled += _ => Fall();

    }
    private void FixedUpdate()
    {
        SetGravity();   
    }
    private void SetGravity()
    {
        if (collision.InGround && rigidbody.velocity.y < 0)
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

    public void TakeDamage(float dmg)
    {
        if (dmg <= 1)
            player.Health -= 1;
        else
            player.Health -= 2;
    }
}