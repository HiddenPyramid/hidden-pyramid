using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveOrigin : MonoBehaviour
{
    public float explosionRadius = 10;
    public float explosionForce = 150;
    public float upwardsModifier = 10f;
    public GameObject particles;

    public void Explode(Rigidbody [] rigidbodies)
    {
        foreach (Rigidbody rb in rigidbodies)
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);

        foreach (Rigidbody rb in rigidbodies)
            rb.GetComponentInParent<Animator>().enabled = true;

        Instantiate(particles, transform.position, Quaternion.identity);
    }
}
