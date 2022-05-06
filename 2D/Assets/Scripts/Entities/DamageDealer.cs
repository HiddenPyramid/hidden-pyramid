using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private bool destroyOnHit;
    private void OnCollisionEnter(Collision collision)
    {
        var reciever = collision.gameObject.GetComponent<ITakeDamage>();
        if(reciever != null)
        {
            reciever.TakeDamage(damage);
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        var reciever = collision.gameObject.GetComponent<ITakeDamage>();
        if (reciever != null)
        {
            reciever.TakeDamage(damage);
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
