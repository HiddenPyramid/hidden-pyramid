using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPausing : MonoBehaviour
{
    private PauseMenu pauseMenu;
    private InputAction pauseAction;
    private static int onCooldown;
    private float cooldown = 0.5f;

    private void Start() {
        pauseAction = FindObjectOfType<PlayerManager>().GetPlayer().GetComponent<PlayerInput>().actions[Parameter.ACTION_PAUSE];
        pauseMenu = FindObjectOfType<PauseMenu>();
        pauseAction.performed += _ => OnPause();
    }

    void OnPause()
    {
        if (PlayerPausing.onCooldown == 0)
        {
            StartCoroutine(CooldownWait());
            GameManager.instance.paused = !GameManager.instance.paused;
            if (GameManager.instance.paused) pauseMenu.EnterPause();
            else pauseMenu.ExitPause();
        }
    }

    private IEnumerator CooldownWait()
    {
        PlayerPausing.onCooldown ++;
        yield return new WaitForSeconds(cooldown);
        PlayerPausing.onCooldown --; 
    }
}
