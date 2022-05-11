using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public GameObject wall_collider;
    public Animator blockade;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("eiiiii");
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("BUM");
            Destroy(wall_collider);
            blockade.SetBool("destroyed", true);
        }
    }
}
