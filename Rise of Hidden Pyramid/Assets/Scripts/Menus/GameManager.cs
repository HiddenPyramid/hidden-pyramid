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
    private void Awake()
    {
        instance = this;
        currentScene = SceneIndexes.MAIN_MENU;
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);

         Debug.Log("a veure");
    }

    public void LoadGame(SceneIndexes nextScene)
    {
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)nextScene, LoadSceneMode.Additive));
        currentScene = nextScene;

        Debug.Log("PATATA");

        StartCoroutine(GetSceneLoadProgressAndActivateScene());
    }

    private IEnumerator GetSceneLoadProgressAndActivateScene()
    {
        Debug.Log("entr");
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                Debug.Log("hei");
                UpdateProgress();
                yield return null;
            }
        }
        Debug.Log("acabat");
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
}
