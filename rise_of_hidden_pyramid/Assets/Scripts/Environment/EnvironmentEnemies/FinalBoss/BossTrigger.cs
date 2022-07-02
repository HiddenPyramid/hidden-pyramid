using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public FinalBoss finalBoss;
    public bool firstTime = true;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(Parameter.PLAYER))
        {
            if (firstTime) finalBoss.ActivateFirstTime();
            else finalBoss.Activate();

            this.firstTime = false;
        }
    }
}
