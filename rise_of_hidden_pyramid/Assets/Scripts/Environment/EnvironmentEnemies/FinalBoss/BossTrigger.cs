using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public FinalBoss finalBoss;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(Parameter.PLAYER))
        {
            finalBoss.Activate();
        }
    }
}
