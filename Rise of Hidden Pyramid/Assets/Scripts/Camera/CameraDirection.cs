using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDirection : MonoBehaviour
{
    public CameraController cameraController;
    private float lastDirection = -1;

    private float input;

    public void SetInput(float newInput)
    {
        if (newInput != 0)
        {
            if (newInput != lastDirection)
            {
                lastDirection = newInput;
                cameraController.SwapXOffset();
            }
        }
    }
}
