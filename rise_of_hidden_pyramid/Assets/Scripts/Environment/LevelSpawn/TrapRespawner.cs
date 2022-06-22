using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRespawner : MonoBehaviour
{
    public Animator trapDoor, headFall1, headFall2;
    
    public void RespawnTrap()
    {
        trapDoor.SetTrigger("respawn");
        headFall1.SetTrigger("respawn");
        headFall2.SetTrigger("respawn");
    }
}
