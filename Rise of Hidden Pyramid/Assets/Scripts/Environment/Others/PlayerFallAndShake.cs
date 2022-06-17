using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallAndShake : MonoBehaviour
{
    public CameraController cameraController;
    public ParticleSystem particleSystem;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(Parameter.PLAYER))
        {
            cameraController.CameraShake();
            particleSystem.Play();
            audioSource.Play();
        }
    }
}
