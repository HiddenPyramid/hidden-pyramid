using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAndSlym [] players;
    public int initialIndex = 0;
    private static PlayerAndSlym [] playersStatic;
    private static int playerIndex = 0;

    private void Awake() 
    {
        playersStatic = players;
        playerIndex = initialIndex;
    }

    public static GameObject GetPlayer()
    {
        return playersStatic[playerIndex].player;
    }

    public static void NextPlayer()
    {
        DisableCurrent();
        playerIndex ++;
        EnableCurrent();
        if (playerIndex >= playersStatic.Length) playerIndex = 0;
    }

    public static void DisableCurrent()
    {
        playersStatic[playerIndex].gameObject.SetActive(false);
    }

    public static void EnableCurrent()
    {
        playersStatic[playerIndex].gameObject.SetActive(true);
    }
}
