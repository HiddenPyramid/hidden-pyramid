using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public MovingPillar movingPillar;
    public Animator animator;

    private void OnTriggerEnter(Collider other) {
        if (hitPlayer(other) || hitEnemy(other))
        {
            movingPillar.Shoot();
            animator.SetTrigger("down");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (hitPlayer(other) || hitEnemy(other))
        {
            animator.SetTrigger("up");
        }
    }

    private bool hitPlayer(Collider other)
    {
        return other.gameObject.layer == LayerMask.NameToLayer("Player");
    }
    private bool hitEnemy(Collider other)
    {
        return other.CompareTag("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Enemy");
    }
}
