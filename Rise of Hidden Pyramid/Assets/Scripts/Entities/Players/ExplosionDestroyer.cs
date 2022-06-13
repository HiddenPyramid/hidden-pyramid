using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    public void WaitToDestroy(GameObject explosion, float time)
    {
        StartCoroutine(WaitAndDestroy(explosion, time));
    }

    private static IEnumerator WaitAndDestroy(GameObject explosion, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(explosion);
    }
}
