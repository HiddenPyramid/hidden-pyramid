using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAndSlym [] players;
    public float changePlayerDelay = 1.5f;
    public int playerIndex = 0;

    public delegate void playerChangeDelegate();
    public event playerChangeDelegate playerChangeEvent;
    private bool isChanging = false;

    public GameObject GetPlayer()
    {
        return players[playerIndex].player;
    }

    public void NextPlayer()
    {
        Debug.Log("Change");
        if (!isChanging)
        {
            StartCoroutine(WaitForNextPlayer());
        }
    }

    private IEnumerator WaitForNextPlayer()
    {
        isChanging = true;
        yield return new WaitForSeconds(changePlayerDelay);
        GoToNextPlayer();
        isChanging = false;
        playerChangeEvent();
    }

    private void GoToNextPlayer()
    {
        DisableCurrent();
        playerIndex ++;
        EnableCurrent();
        if (playerIndex >= players.Length) playerIndex = 0;
    }

    public void DisableCurrent()
    {
        players[playerIndex].gameObject.SetActive(false);
    }

    public void EnableCurrent()
    {
        players[playerIndex].gameObject.SetActive(true);
    }
}
