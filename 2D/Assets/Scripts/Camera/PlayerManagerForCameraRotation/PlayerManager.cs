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
        playerIndex ++;
        if (playerIndex >= playersStatic.Length) playerIndex = 0;
    }
}
