using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public GameObject wall_collider;
    public Animator blockade;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(wall_collider);
            blockade.SetBool("destroyed", true);
        }
    }
}
