using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDirection : MonoBehaviour
{
    public CameraController cameraController;
    private float lastInput; 

    public void SetInput(float newInput)
    {
        if (newInput != 0 && lastInput != newInput) cameraController.SmoothDamp();
        if (newInput != 0) lastInput = newInput;

        if (newInput == 1) cameraController.PositiveXOffset();
        else if (newInput == -1) cameraController.NegativeXOffset();
    }
}
