using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    public EntranceDoor entranceDoor;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Parameter.PLAYER))
            entranceDoor.CloseDoor();
    }
}
