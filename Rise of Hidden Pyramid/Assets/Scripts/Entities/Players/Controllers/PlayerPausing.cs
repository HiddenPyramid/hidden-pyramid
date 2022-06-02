using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPausing : MonoBehaviour
{
    private PauseMenu pauseMenu;
    private InputAction pauseAction;

    private void Start() {
        pauseAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_PAUSE];
        pauseMenu = GetComponent<PauseMenu>();
        pauseAction.performed += _ => OnPause();
    }

    void OnPause()
    {
        Debug.Log("Pausing");
        GameManager.paused = !GameManager.paused;
        if (GameManager.paused) pauseMenu.EnterPause();
        else pauseMenu.ExitPause();
    }
}
