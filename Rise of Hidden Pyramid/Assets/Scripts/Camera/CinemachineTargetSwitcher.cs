using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTargetSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public Transform target;

    private void Start() {
        Debug.Log("comenca00");
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("entra algú");
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("canvi");
            camera.m_LookAt = target;
        }    
    }
}
