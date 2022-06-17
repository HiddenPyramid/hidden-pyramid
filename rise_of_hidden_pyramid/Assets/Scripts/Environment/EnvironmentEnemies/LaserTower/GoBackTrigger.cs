using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackTrigger : MonoBehaviour
{
    public Vector3 direction;
    public float force = 3f;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Parameter.PLAYER))
        {
            GoBack(other.gameObject);
        }
    }

    private void GoBack(GameObject player)
    {
        player.GetComponent<Rigidbody>().AddForce(direction*force);
    }
}
