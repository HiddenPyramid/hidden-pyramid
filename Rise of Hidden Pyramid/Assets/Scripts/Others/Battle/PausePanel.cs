using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image image; 
    [SerializeField] private GameObject pausePanelAssets;

    private void Start() 
    {
        image.enabled = false;
        pausePanelAssets.SetActive(false);
    }

    public void Pause()
    {
        image.enabled = true;
        pausePanelAssets.SetActive(true);
        // TODO PAUSE GAME
    }

    public void Resume()
    {
        image.enabled = false;
        pausePanelAssets.SetActive(false);
        // TODO RESUME GAME
    }

    public void PrepareToQuit()
    {
        // TODO RESUME GAME
    }
}
