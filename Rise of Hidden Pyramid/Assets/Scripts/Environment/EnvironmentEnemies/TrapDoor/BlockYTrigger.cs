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
            Debug.Log("ESIIII IPERFECTE Que HO FA");
            cameraController.BlockY();
        }
    }
}
