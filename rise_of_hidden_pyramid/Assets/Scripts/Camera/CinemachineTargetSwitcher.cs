using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTargetSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public Transform target;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("MainCamera"))
        {
            camera.m_LookAt = target;
        }    
    }
}
