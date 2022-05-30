using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    public bool opened = true;
    public Animator animator;
    public GameObject doorCollider;

    public void OpenDoor()
    {
        if(!opened)
        {
            opened = true;
            animator.SetTrigger("open");
            doorCollider.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        if(opened)
        {
            opened = false;
            animator.SetTrigger("close");
            doorCollider.SetActive(true);
        }
    }
}
