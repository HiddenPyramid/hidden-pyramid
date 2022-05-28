using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MoveCameraTrigger : MonoBehaviour
{
    // Note: Intended usage: the trigger covers all the length of the corridor.
    public CameraController cameraController;
    public Vector3 offset;

    private void OnTriggerEnter(Collider other) 
    {
        MoveIfPlayerEntered(other);        
    }    

    private void MoveIfPlayerEntered(Collider other)
    {
        if (other.gameObject.CompareTag(Parameter.PLAYER))
            MoveCamera();
    } 

    private void MoveCamera()
    {
        cameraController.SetOffset(this.offset);
    }
    
}
