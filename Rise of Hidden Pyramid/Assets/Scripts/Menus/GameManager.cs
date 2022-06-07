using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static SceneIndexes currentScene;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private ProgressBar bar;
    public bool paused = false;
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public Animator curtainAnimator;
    public float curtainCloseTime = 0.35f;

    private void Awake()
    {
        instance = this;
        currentScene = SceneIndexes.MAIN_MENU;
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    public void LoadGame(SceneIndexes nextScene)
    {
        loadingScreen.gameObject.SetActive(true);
        curtainAnimator.SetTrigger("openCurtainFast");

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)nextScene, LoadSceneMode.Additive));
        currentScene = nextScene;
        StartCoroutine(FAKE_SMOOTH_GetSceneLoadProgressAndActivateScene());
    }

    //////////////////////////////////////////////////////////////// REAL PROGRESS BUT UNSMOOTH RESULT
    
    private IEnumerator GetSceneLoadProgressAndActivateScene()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                UpdateProgress();
                yield return null;
            }
        }
        curtainAnimator.SetTrigger("closeCurtainFast");
        yield return new WaitForSeconds(curtainCloseTime);
        loadingScreen.gameObject.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(((int)currentScene)));
    }

    private void UpdateProgress()
    {
        float totalSceneProgress = 0;
        foreach (AsyncOperation operation in scenesLoading)
        {
            totalSceneProgress += operation.progress;
        }
        totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
        bar.UpdateCurrent(Mathf.RoundToInt(totalSceneProgress));
    }

    //////////////////////////////////////////////////////////////// FAKE PROGRESS BUT SMOOTH RESULT
    private IEnumerator FAKE_SMOOTH_GetSceneLoadProgressAndActivateScene()
    {
        float totalSceneProgress = 0;
        while (totalSceneProgress < 100f)
        {
            totalSceneProgress += Time.deltaTime / scenesLoading.Count / 10;
            bar.UpdateCurrent(Mathf.RoundToInt(totalSceneProgress));
        }

        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return new WaitForEndOfFrame(); // Without classic UpdateProgress
            }
        }
        curtainAnimator.SetTrigger("closeCurtainFast");
        yield return new WaitForSeconds(curtainCloseTime);
        loadingScreen.gameObject.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(((int)currentScene)));
    }

}
