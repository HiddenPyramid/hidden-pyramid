using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTrigger : MonoBehaviour
{
    public AIPatrol aipatrol;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Parameter.PLAYER))
        {
            Debug.Log("He arribat");
            aipatrol.Flip();
        }
    }
}
