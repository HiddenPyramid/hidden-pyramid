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

    public void Stop()
    {
        this.animator.SetTrigger("stop");
    }

    public void Defeated()
    {
        this.animator.SetBool("defeated", true);
    }
}
