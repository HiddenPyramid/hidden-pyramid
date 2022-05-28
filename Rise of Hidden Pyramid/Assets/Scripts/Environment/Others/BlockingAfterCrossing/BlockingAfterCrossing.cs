using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BlockingAfterCrossing : MonoBehaviour
{
    public Animator animator;
    private bool blocked = false;

    public GameObject blockage;

    private void Start() 
    {
        blockage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (!blocked && other.CompareTag(Parameter.PLAYER))
        {
            animator.SetTrigger("block");
            this.blocked = true;
            blockage.SetActive(true);
        }
    }
}
