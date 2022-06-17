using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDamageOnDie : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Collider[] playerColliders;

    public void StopReceivingDamage()
    {
        playerRigidbody.isKinematic = true;
        foreach (Collider playerCollider in playerColliders)
            playerCollider.enabled = false;
    }

    public void RestartReceivingDamage()
    {
        playerRigidbody.isKinematic  = false;
        foreach (Collider playerCollider in playerColliders)
            playerCollider.enabled = true;
    }
}
