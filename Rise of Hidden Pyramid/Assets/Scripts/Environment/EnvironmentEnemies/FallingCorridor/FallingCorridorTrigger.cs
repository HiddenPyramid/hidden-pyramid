using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FallingCorridorTrigger : MonoBehaviour
{
    public Animator animator;
    private bool triggerEnabled = true;
    public PlayerManager playerManager;
    public CameraController cameraController;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && triggerEnabled)
        {
            animator.SetTrigger("fall");
            this.triggerEnabled = false;

            MoveShadowY();
            UnblockCamera();
        }
    }

    private void MoveShadowY()
    {
        playerManager.GetPlayer().GetComponentInChildren<ShadowMovement>().NextYPosition();
    }

    private void UnblockCamera()
    {
        cameraController.UnblockY();
    }
}
