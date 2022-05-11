using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    public Animator animator;

    public void Shoot()
    {
        this.animator.SetTrigger("shoot");
    }
}
