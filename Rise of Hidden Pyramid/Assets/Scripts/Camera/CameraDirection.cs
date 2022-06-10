using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDirection : MonoBehaviour
{
    public CameraController cameraController;

    public void SetInput(float newInput)
    {
        Debug.Log(newInput);
        if (newInput == 1) cameraController.PositiveXOffset();
        else if (newInput == -1) cameraController.NegativeXOffset();
    }
}
