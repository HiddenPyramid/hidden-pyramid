using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static SceneIndexes currentScene;
    [SerializeField] private Animator loadingScreen;
    [SerializeField] private ProgressBar bar;

    public bool paused = false;
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    private AsyncOperation nextSceneOperation;

    public Animator curtainAnimator;
    public float curtainCloseTime = 0.35f;
    public GameObject canvas;

    public static bool isCredits = false;
    public static bool whiteCurtain = false;

    private void Awake()
    {
        instance = this;
        currentScene = SceneIndexes.MAIN_MENU;
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    public void LoadGame(SceneIndexes nextScene)
    {
        loadingScreen.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);

        loadingScreen.SetTrigger("fakeLoad");
        curtainAnimator.SetTrigger("openCurtainFast");


        UnloadAndLoadScenes(nextScene);

        StartCoroutine(FAKE_GetSceneLoadProgressAndActivateScene());
    }

    private void UnloadAndLoadScenes(SceneIndexes nextScene)
    {
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)currentScene));
        nextSceneOperation = SceneManager.LoadSceneAsync((int)nextScene, LoadSceneMode.Additive);
        nextSceneOperation.allowSceneActivation = false;
        scenesLoading.Add(nextSceneOperation);

        currentScene = nextScene;
    }
    
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
        canvas.gameObject.SetActive(false);

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



    ///////////////////// FAKE LOADING
    private IEnumerator FAKE_GetSceneLoadProgressAndActivateScene()
    {
        
        yield return new WaitForSeconds(5f);//loadingScreen.GetCurrentAnimatorStateInfo(0).length);
        nextSceneOperation.allowSceneActivation = true;

        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                //UpdateProgress();
                yield return null;
            }
        }
        curtainAnimator.SetTrigger("closeCurtainFast");
        yield return new WaitForSeconds(curtainCloseTime);
        

        loadingScreen.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(((int)currentScene)));
    }

}
