using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject [] players;
    private static GameObject [] playersStatic;
    private static int playerIndex = 0;

    private void Awake() 
    {
        playersStatic = players;
    }

    public static GameObject GetPlayer()
    {
        return playersStatic[playerIndex];
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
        playersStatic[playerIndex].SetActive(false);
    }

    public static void EnableCurrent()
    {
        playersStatic[playerIndex].SetActive(true);
    }
}
