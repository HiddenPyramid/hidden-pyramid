using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHead : MonoBehaviour
{
    public Animator headAnimator;
    public ParticleSystem dustParticles;
    public AudioSource fallingAudio, menacingAudio;
    public CameraController cameraController;
    public float shakeDelay = 0.2f;

    private bool hasFallen = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (!hasFallen && IsPlayer(other))
        {
            hasFallen = true;
            FallHead();
            ThrowParticles();
            PlayAudio();
            StartCoroutine(ShakeCamera());
        }
    }

    private bool IsPlayer(Collider other) { return other.CompareTag(Parameter.PLAYER); }

    private void FallHead() { this.headAnimator.SetTrigger("fall");}

    private void ThrowParticles() { this.dustParticles.Play(); }

    private void PlayAudio() { this.fallingAudio.Play(); }

    private IEnumerator ShakeCamera() { 
        yield return new WaitForSeconds(shakeDelay); 
        this.cameraController.cameraShakingNoDamage = true; 
        this.menacingAudio.Play();
    }
}
