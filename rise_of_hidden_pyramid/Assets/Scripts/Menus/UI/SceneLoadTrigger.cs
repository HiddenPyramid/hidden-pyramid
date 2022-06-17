using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneIndexes nextScene;
    [SerializeField] private PausePanel optionalPausePanel;
    [SerializeField] private AudioSource optionalButtonAudio;
    [SerializeField] private float waitDuration = 3f;
    public Animator curtainAnimator;
    [SerializeField] private AudioSource optionalAudioToFadeOut;
    public float smoothTime = 3f;

    public bool whiteCurtain = false;

    private void Start() {
        GameManager.whiteCurtain = whiteCurtain;
    }

    public void LoadNextScene()
    {
        if (optionalPausePanel != null) optionalPausePanel.PrepareToQuit();
        TryToCloseCurtain();
        StartCoroutine(LoadGameWaiting());
    }

    private void TryToCloseCurtain()
    {
        if (curtainAnimator != null)
        {
            if (GameManager.whiteCurtain) curtainAnimator.SetBool("white", true);
            curtainAnimator.SetTrigger("close");
        }
    }

    private IEnumerator LoadGameWaiting()
    {
        // FadeOutAudio
        if (optionalAudioToFadeOut != null)
        {
            float currentTime = 0f;
            while (optionalAudioToFadeOut.volume > 0.001f)
            {
                currentTime += Time.deltaTime;
                optionalAudioToFadeOut.volume = Mathf.Lerp(optionalAudioToFadeOut.volume, 0f, currentTime/smoothTime);
                yield return new WaitForEndOfFrame();
            }
            optionalAudioToFadeOut.volume = 0f;
        }

        // LoadGameWaiting
        float duration = PlayAndGetAudioDuration();
        yield return new WaitForSeconds(duration);
        duration = waitDuration > duration ? waitDuration : duration;

        GameManager.instance.LoadGame(nextScene);
    }

    private float PlayAndGetAudioDuration()
    {
        float duration = 0;
        if (optionalButtonAudio != null)
        {
            duration = optionalButtonAudio.clip.length;
            optionalButtonAudio.PlayOneShot(optionalButtonAudio.clip);
        }
        return duration;
    }
}
