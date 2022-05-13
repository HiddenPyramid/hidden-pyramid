using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats stats;
    private Rigidbody rigidbody;
    private InputAction moveAction;
    private float input;
    private Vector3 push;
    //private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        moveAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_MOVE];
        //animator = GetComponentInChildren<Animator>();

        moveAction.performed += x => input = x.ReadValue<Vector2>().x;
        moveAction.canceled += _ => input = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        Vector3 l_playerMovement = input * transform.right * stats.Speed;
        l_playerMovement.y = rigidbody.velocity.y;
        rigidbody.velocity = l_playerMovement;
        //animator.SetFloat("Speed", Mathf.Abs(l_playerMovement.x));
    }
    public void Push(Vector3 vec)
    {
        Debug.Log(vec);
        push = vec;
        rigidbody.AddForce(push, ForceMode.Impulse);
    }

}
