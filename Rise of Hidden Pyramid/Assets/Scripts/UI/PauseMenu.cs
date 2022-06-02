using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Animator [] UItoDeactivateOnPause;
    public Animator [] UItoActivateOnPause;

    public Animator playAnimator, pauseAnimator;
    public Animator pauseMenuAnimator;

    public void EnterPause()
    {
        DeactivateUI();
    }

    public void ExitPause()
    {
        ReactivateUI();
    }

    private void DeactivateUI()
    {
        foreach (Animator UIanimator in UItoDeactivateOnPause)
        {
            UIanimator.gameObject.SetActive(true);
            UIanimator.SetBool("paused", true);
        }
        foreach (Animator UIanimator in UItoActivateOnPause)
        {
            UIanimator.gameObject.SetActive(true);
            UIanimator.SetBool("paused", false);
        }

        playAnimator.gameObject.SetActive(true);
        pauseAnimator.gameObject.SetActive(false);

        pauseMenuAnimator.SetBool("paused", true);
        playAnimator.SetBool("paused", false);
        playAnimator.SetBool("restarted", true);
        pauseAnimator.SetBool("paused", true);
    }

    private void ReactivateUI()
    {
        foreach (Animator UIanimator in UItoDeactivateOnPause)
        {
            UIanimator.gameObject.SetActive(true);
            UIanimator.SetBool("paused", false);
        }
        foreach (Animator UIanimator in UItoActivateOnPause)
        {
            UIanimator.gameObject.SetActive(true);
            UIanimator.SetBool("paused", true);
        }
        
        playAnimator.gameObject.SetActive(false);
        pauseAnimator.gameObject.SetActive(true);

        pauseMenuAnimator.SetBool("paused", false);
        pauseAnimator.SetBool("restarted", true);
        playAnimator.SetBool("paused", true);
        pauseAnimator.SetBool("paused", false);
    }
}
