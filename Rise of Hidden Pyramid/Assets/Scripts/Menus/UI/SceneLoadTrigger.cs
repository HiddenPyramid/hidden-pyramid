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

    public void LoadNextScene()
    {
        if (optionalPausePanel != null) optionalPausePanel.PrepareToQuit();
        TryToCloseCurtain();
        StartCoroutine(LoadGameWaiting());
    }

    private void TryToCloseCurtain()
    {
        if (curtainAnimator != null)
            curtainAnimator.SetTrigger("close");
    }

    private IEnumerator LoadGameWaiting()
    {
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