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

    public CameraDirection cameraDirection;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        moveAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_MOVE];

        moveAction.performed += x => input = x.ReadValue<Vector2>().x;
        moveAction.canceled += _ => input = 0f;
    }

    void FixedUpdate()
    {
        Move();
        cameraDirection.SetInput(input);
    }


    private void Move()
    {
        Vector3 l_playerMovement =  input * transform.right * stats.Speed;
        l_playerMovement.y = rigidbody.velocity.y;
        rigidbody.velocity = l_playerMovement + push;
        push = Vector3.zero;
        Debug.Log("PAtata");
    }
    public void Push(Vector3 vec)
    {
        push = vec;
    }

}
