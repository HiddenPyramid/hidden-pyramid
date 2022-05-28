using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private Transform GroundPoint;
    [SerializeField]
    private Transform WallPoint;

    private Picking pickSystem;

    [SerializeField]
    private float pointRadius = 0.15f;
    [SerializeField]
    private float wallPointRadius = 0.6f;
    private bool inGround;
    private bool inWall;

    public bool InGround { get => inGround; }
    public bool InWall { get => inWall; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundPoint.position, pointRadius);
        Gizmos.DrawWireSphere(WallPoint.position, wallPointRadius);
        Gizmos.color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        pickSystem = GetComponent<Picking>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        IsInGround();
        IsInWall();
    }


    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.layer)
        {
            case Parameter.LAYER_ITEM:
                pickSystem.Pickable = collider.transform;
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
    private void IsInWall()
    {
        inWall = Physics.Raycast(WallPoint.position, transform.right, wallPointRadius) ||
            Physics.Raycast(WallPoint.position, -transform.right, wallPointRadius);
    }
}


