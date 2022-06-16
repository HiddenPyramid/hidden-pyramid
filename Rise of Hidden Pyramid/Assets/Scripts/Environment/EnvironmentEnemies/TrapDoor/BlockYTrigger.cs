using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockYTrigger : MonoBehaviour
{
    public CameraController cameraController;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraController.BlockY();
        }
    }
}
