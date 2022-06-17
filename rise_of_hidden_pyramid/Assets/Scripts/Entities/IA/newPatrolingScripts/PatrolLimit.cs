using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLimit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        AIPatrol aiPatrol = null;
        try{ aiPatrol = other.GetComponent<AIPatrol>();
        } catch {}
        
        if (aiPatrol != null)
        {
            aiPatrol.Flip();
        }
    }
}
