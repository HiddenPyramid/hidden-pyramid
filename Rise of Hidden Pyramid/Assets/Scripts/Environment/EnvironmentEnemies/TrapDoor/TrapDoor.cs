using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public Animator animator;
    private bool triggerEnabled = true;
    public PlayerManager playerManager;
    public CameraController cameraController;

    public float waitTime = 3f;
    
    public void OpenTrapDoor()
    {
        StartCoroutine(WaitForTrap());
    }

    private IEnumerator WaitForTrap()
    {
        yield return new WaitForSeconds(waitTime);

        if (triggerEnabled)
        {
            animator.SetTrigger("fall");
            this.triggerEnabled = false;

            MoveShadowY();
            UnblockCamera();
        }
    }

    private void MoveShadowY()
    {
        playerManager.GetPlayer().GetComponentInChildren<ShadowMovement>().NextYPosition();
    }

    private void UnblockCamera()
    {
        cameraController.UnblockY();
    }
}