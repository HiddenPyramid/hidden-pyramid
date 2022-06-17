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
        if (newInput == 1 && lastInput != newInput) cameraController.SwapPositiveXOffset();
        else if (newInput == -1 && lastInput != newInput) cameraController.SwapNegativeXOffset();
        if (newInput != 0) lastInput = newInput;
    }
}
