using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsLoader : MonoBehaviour
{
    public AudioClip menuAudio, creditsAudio;
    public AudioSource audioSource;
    public GameObject gameLogo, gameCredits;

    private void Start() 
    {
        if (GameManager.isCredits) PrepareCredits();
        else PrepareMenu();
    }

    private void PrepareCredits()
    {
        audioSource.clip = creditsAudio;
        audioSource.Play();
        gameLogo.SetActive(false);
        gameCredits.SetActive(true);
        SetBackToMenuInCasePlayerReplays();
    }

    private void PrepareMenu()
    {
        audioSource.clip = menuAudio;
        audioSource.Play();
        gameLogo.SetActive(true);
        gameCredits.SetActive(false);
    }

    private void SetBackToMenuInCasePlayerReplays()
    {
        GameManager.isCredits = false;
    }
}
