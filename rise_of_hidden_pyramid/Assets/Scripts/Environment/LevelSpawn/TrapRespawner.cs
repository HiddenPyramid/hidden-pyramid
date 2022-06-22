using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRespawner : MonoBehaviour
{
    public Animator trapDoor;
    public FallingHead headFall1, headFall2;
    public CallTrapAndSpawnLevel callTrapAndSpawnLevel;
    public CameraController cameraController;
    
    public void RespawnTrap()
    {
        trapDoor.SetTrigger("respawn");
        headFall1.Respawn();
        headFall2.Respawn();
        callTrapAndSpawnLevel.Respawn();
        cameraController.GoToPreviouslyFixedCamera();
    }
}
