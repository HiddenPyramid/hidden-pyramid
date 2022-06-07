using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip nextClip;

    public float smoothTime = 0.7f;
    float velocity = 0.0f;
    public float delayTime = 0.3f;
    public float maxVolume = 1f;

    public bool alreadySwitched = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (!alreadySwitched &&  other.gameObject.CompareTag(Parameter.PLAYER))
        {
            alreadySwitched = true;
            SwitchMusic();
        }
    }

    private void SwitchMusic()
    {
        StartCoroutine(FadeOutClip());
        StartCoroutine(LoadNextClip());
    }

    private IEnumerator FadeOutClip()
    {
        while (audioSource.volume > 0.1f)
        {
            audioSource.volume = Mathf.SmoothDamp(audioSource.volume, 0f, ref velocity, smoothTime);
            yield return new WaitForEndOfFrame();
        }
        audioSource.volume = 0f;
    }

    private IEnumerator LoadNextClip()
    {
        yield return new WaitForSeconds (smoothTime + delayTime);
        audioSource.clip = nextClip;
        audioSource.volume = maxVolume;
    }
}
