using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockYTrigger : MonoBehaviour
{
    public CameraController cameraController;
    private bool alreadyBlocked = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && !alreadyBlocked)
        {
            alreadyBlocked = true;
            cameraController.BlockY();
        }
    }
}
