using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    public bool opened = true;
    public Animator animator;
    public GameObject doorCollider;
    public GameObject lightsToDeactivate;

    public void OpenDoor()
    {
        if(!opened)
        {
            opened = true;
            animator.SetTrigger("open");
            doorCollider.SetActive(false);
            if (lightsToDeactivate != null) lightsToDeactivate.SetActive(true);
        }
    }

    public void CloseDoor()
    {
        if(opened)
        {
            opened = false;
            animator.SetTrigger("close");
            doorCollider.SetActive(true);
            if (lightsToDeactivate != null) lightsToDeactivate.SetActive(false);
        }
    }
}
