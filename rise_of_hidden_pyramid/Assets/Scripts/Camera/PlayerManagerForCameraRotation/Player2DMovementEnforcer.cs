using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovementEnforcer : MonoBehaviour
{
    public PlayerAndSlym playerAndSlym;
    public Axis currentAxis;
    public float checkInterval = 1.0f;
    public float current2Dposition = 0f;
    
    private bool active = true;

    private void Start()
    {
        StartCoroutine(EnforcePlayer2DMovement());
    }
    
    private IEnumerator EnforcePlayer2DMovement()
    {
        while (active)
        {
            yield return new WaitForSeconds(checkInterval);
            EnforcePosition();
        }
        yield return new WaitForSeconds(checkInterval);
    }

    private void EnforcePosition()
    {
        GameObject player = playerAndSlym.player;
        switch (playerAndSlym.axisToEnforce2DMovement)
        {
            case Axis.x:
                player.transform.position = new Vector3 (current2Dposition, player.transform.position.y, player.transform.position.z);
            break;
            case Axis.y:
                player.transform.position = new Vector3 (player.transform.position.x, current2Dposition, player.transform.position.z);
            break;
            case Axis.z:
                player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, current2Dposition);
            break;
        }
    }

    public void SetPlayer(PlayerAndSlym newPlayer)
    {
        this.playerAndSlym = newPlayer;
    }

    public void SetPosition(float newPosition, Axis newAxis)
    {
        this.current2Dposition = newPosition;
        this.currentAxis = newAxis;
    }

    public enum Axis
    {
        x, y, z
    }
}
