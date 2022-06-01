using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTrigger : MonoBehaviour
{
    public AIPatrol aipatrol;
    public bool left;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Parameter.PLAYER))
        {
            aipatrol.Flip(left);
        }
    }
}
