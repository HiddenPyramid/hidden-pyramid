using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float smoothMove = 2f;
    public float smoothRotation = 10f;
    public float changingDistance = 1.5f;
    public Transform [] targets;
    public int currentTarget = 0;
    private bool moving = true;
    private bool remainingTargets => currentTarget < targets.Length;
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (TargetReached())
            NextTarget();
        
        if (moving && remainingTargets)
        {
            Vector3 vel = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, targets[currentTarget].position, smoothMove * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targets[currentTarget].rotation, Time.deltaTime / smoothRotation);
        }
    }

    private bool TargetReached()
    {
        if (remainingTargets)
        {
            float distance = (transform.position - targets[currentTarget].position).magnitude; 
            return distance < changingDistance;
        }
        return true;
    }

    private void NextTarget()
    {
        this.currentTarget ++;
    }
}
