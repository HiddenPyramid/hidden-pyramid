using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTriggersMovement : MonoBehaviour
{
    public Transform golem;
    
    void FixedUpdate()
    {
        transform.position = golem.position;
    }
}
