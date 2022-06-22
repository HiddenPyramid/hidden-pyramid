using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverOnTrappedd : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Transform movePosition;

    public void MovePlayer(GameObject player)
    {
        player.transform.position = movePosition.position;
        particleSystem.Play();
    }
}
