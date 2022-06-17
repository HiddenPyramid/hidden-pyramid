using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip nextClip;

    public float smoothTime = 2f;
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
        StartCoroutine(FadeOutClipLoadNextClip());
    }

    private IEnumerator FadeOutClipLoadNextClip()
    {
        // FadeOutClip
        float currentTime = 0f;
        while (audioSource.volume > 0.009f)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, currentTime/smoothTime);
            yield return new WaitForEndOfFrame();
        }
        audioSource.volume = 0f;

        audioSource.transform.position = this.transform.position;

        // LoadNextClip
        yield return new WaitForSeconds (smoothTime);
        audioSource.gameObject.SetActive(false);
        yield return new WaitForSeconds (delayTime);
        audioSource.gameObject.SetActive(true);
        audioSource.clip = nextClip;
        audioSource.volume = maxVolume;
    }
}
