using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLaser : MonoBehaviour
{
    public Animator animator;

    public void Shoot()
    {
        this.animator.SetTrigger("shoot");
    }
}
