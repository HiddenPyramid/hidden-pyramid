using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPyramidShake : MonoBehaviour
{
    public Animator shakeAnimator;
    public float timeToShake = 18f;

    private void Start() 
    {
        StartCoroutine(WaitToStopShaking());
    }

    private IEnumerator WaitToStopShaking()
    {
        yield return new WaitForSeconds(timeToShake);
        shakeAnimator.enabled = false;
    }
}
