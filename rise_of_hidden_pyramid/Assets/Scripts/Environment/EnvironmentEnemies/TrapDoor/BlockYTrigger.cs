using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockYTrigger : MonoBehaviour
{
    public CameraController cameraController;
    private bool alreadyBlocked = false;

    public PlayerManager optionalPlayerManagerToUnblockMovement;


    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && !alreadyBlocked)
        {
            alreadyBlocked = true;
            cameraController.BlockY();
            UnblockPlayerMovement();
        }
    }

    private void UnblockPlayerMovement()
    {
        if (optionalPlayerManagerToUnblockMovement != null) 
        {
            PlayerMovement playerMovement = optionalPlayerManagerToUnblockMovement.GetPlayer().GetComponent<PlayerMovement>();
            playerMovement.moveBlocked = false;
        } 
    }
}
