using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    protected Rigidbody rigidbody;
    protected Collider collider;
    protected InputAction action = new InputAction();
    protected float dropFwForce = 4f;
    protected float dropUpForce = 2f;
    public abstract void Attack(InputAction.CallbackContext callback);

    public void Drop()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        collider.isTrigger = false;
        Vector3 vec = Vector3.Scale(transform.right, new Vector3(dropFwForce, 0f, dropFwForce));
        vec.y = dropUpForce;
        rigidbody.AddForce(vec, ForceMode.Impulse);
    }

    public void PickUp()
    {
        transform.localPosition = new Vector3(1f, 0f, 0f);
        transform.rotation = transform.parent.rotation;
    }


    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == Parameter.LAYER_GROUND)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            collider.isTrigger = true;
            transform.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {

    }
}
