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

    public Player2DMovementEnforcer player2DMovementEnforcer;

    private void Awake()
    {
        UpdateDataToEnforcePlayer2DMovement();
    }

    public GameObject GetPlayer()
    {
        return players[playerIndex].player;
    }

    public void NextPlayer()
    {
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
        players[playerIndex].shoots = false;
    }

    public void EnableCurrent()
    {
        players[playerIndex].gameObject.SetActive(true);
        players[playerIndex].shoots = true;
        UpdateDataToEnforcePlayer2DMovement();
    }

    private void UpdateDataToEnforcePlayer2DMovement()
    {
        player2DMovementEnforcer.SetPlayer(players[playerIndex]);
        GameObject currentPlayer = players[playerIndex].player;

        switch (players[playerIndex].axisToEnforce2DMovement)
        {
            case Player2DMovementEnforcer.Axis.x:
                player2DMovementEnforcer.SetPosition(currentPlayer.transform.position.x, Player2DMovementEnforcer.Axis.x);
            break;
            case Player2DMovementEnforcer.Axis.y:
                player2DMovementEnforcer.SetPosition(currentPlayer.transform.position.y, Player2DMovementEnforcer.Axis.y);
            break;
            case Player2DMovementEnforcer.Axis.z:
                player2DMovementEnforcer.SetPosition(currentPlayer.transform.position.z, Player2DMovementEnforcer.Axis.z);
            break;
        }
    }

    public float GetHealth()
    {
        return GetPlayer().GetComponent<HealthSystem>().GetHealth();
    }
}
