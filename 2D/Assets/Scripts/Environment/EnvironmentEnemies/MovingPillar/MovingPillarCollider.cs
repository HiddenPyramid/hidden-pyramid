using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MovingPillarCollider : MonoBehaviour
{
    public Animator animator;
    public void Shoot()
    {
        animator.SetTrigger("shoot");
    }

    public void Unshoot()
    {
        animator.SetTrigger("unshoot");
    }
}
