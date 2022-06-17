using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public EntranceDoor entranceDoor;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Parameter.PLAYER))
            entranceDoor.OpenDoor();
    }
}
