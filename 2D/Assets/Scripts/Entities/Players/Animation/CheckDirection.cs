using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CheckDirection : MonoBehaviour
{
    [SerializeField]
    private Transform visuals;
    private Transform shadowVisuals;
    private PlayerInput input;
    private InputAction aimAction;
    private Vector2 direction;
    private Vector3 mouseDirection;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponentInParent<PlayerInput>();
        aimAction = input.actions[Parameter.ACTION_AIM];
        aimAction.performed += x => direction = x.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        Vector3 dir = new Vector3(direction.x, direction.y, Camera.main.transform.position.z);
        mouseDirection = Camera.main.ScreenToWorldPoint(dir) - transform.position;
        mouseDirection.Normalize();
        
        if (transform.right == Vector3.right){
            visuals.localScale = new Vector3(Mathf.Sign(mouseDirection.x), visuals.localScale.y, visuals.localScale.z);
            shadowVisuals.localScale = new Vector3(Mathf.Sign(mouseDirection.x), shadowVisuals.localScale.y, shadowVisuals.localScale.z);
            }
        else if( transform.right == Vector3.forward){
            visuals.localScale = new Vector3(Mathf.Sign(mouseDirection.z), visuals.localScale.y, visuals.localScale.z);
            shadowVisuals.localScale = new Vector3(Mathf.Sign(mouseDirection.z), shadowVisuals.localScale.y, shadowVisuals.localScale.z);
        }
    }

}
