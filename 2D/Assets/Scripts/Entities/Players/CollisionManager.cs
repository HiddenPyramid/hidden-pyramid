using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private Transform GroundPoint;

    private Picking pickSystem;
    private HealthSystem healthSystem;

    private float pointRadius = 0.15f;
    private bool inGround;
    private bool wasGrounded;

    public bool InGround { get => inGround; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundPoint.position, pointRadius);
        Gizmos.color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        pickSystem = GetComponent<Picking>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsInGround();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Triggered");
        switch (collider.gameObject.layer)
        {
            case Parameter.LAYER_ITEM:
                pickSystem.Pickable = collider.transform;
                break;
            case Parameter.LAYER_TRIGGER:
                healthSystem.Partner = collider.gameObject.GetComponentInParent<HealthSystem>();
                break;
        }
    }
    private void IsInGround()
    {
        inGround = Physics.Raycast(GroundPoint.position, -transform.up, pointRadius);
        //if (!wasGrounded && inGround)
        //    EventManager.Landing();
        //wasGrounded = inGround;
    }
}


