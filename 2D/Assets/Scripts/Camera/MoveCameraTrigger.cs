using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MoveCameraTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public Vector3 offset;
    private Vector3 previousOffset;
    private bool entering = true;
    private void Update() {
        Debug.Log(cameraController.GetOffset());
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag(Parameter.PLAYER))
        {
            MoveCamera();
            Debug.Log("ENTRAA");
        }
    }

    /* private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag(Parameter.PLAYER))
        {
            MoveCamera();
            Debug.Log("SURT");
        }
    } */
    

    private void MoveCamera()
    {
        if (entering)
        {
            entering = false;
            previousOffset = cameraController.GetOffset();
            Debug.Log(cameraController.GetOffset());
            cameraController.SetOffset(this.offset);
            Debug.Log(cameraController.GetOffset());
        }
        else
        {
            entering = true;
            cameraController.SetOffset(this.previousOffset);
        }
    }

}
