using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillar : MonoBehaviour
{
    public bool moving = false;
    public Animator animator;
    public Transform pointA, pointB;
    public float slowness = 100f;
    public Vector3 velocity = Vector3.zero;
    public float changingDistance = 1.5f;
    public MovingPillarCollider pillarCollider;
    public ParticleSystem fire;

    private void Update() 
    {
        if (moving)
        {
            Move();
            if (TargetReached()) TargetChange();
        }
    }

    private void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, pointA.position, ref velocity, slowness * Time.deltaTime);
    }
    
    public void Shoot() {
        if (!moving)
        {
            this.animator.SetTrigger("move");
            moving = true;
        }
        this.fire.Play();
        this.animator.SetTrigger("shoot");
        pillarCollider.Shoot();
        StartCoroutine(shootUnshoot());
    }

    private IEnumerator shootUnshoot()
    {
        pillarCollider.Shoot();
        yield return new WaitForSeconds(3);
        pillarCollider.Unshoot();

    }
    public void Unshoot()
    {
        pillarCollider.Unshoot();
    }

    private bool TargetReached()
    {
        float distance = (transform.position - pointA.position).magnitude; 
        return distance < changingDistance;
    }

    private void TargetChange()
    {
        Transform auxiliar = this.pointA;
        this.pointA = this.pointB;
        this.pointB = auxiliar;
    }
}
