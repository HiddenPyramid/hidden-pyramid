using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FallingCorridorTrigger : MonoBehaviour
{
    public Animator animator;
    private bool triggerEnabled = true;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && triggerEnabled)
        {
            animator.SetTrigger("fall");
            this.triggerEnabled = false;
        }
    }
}
