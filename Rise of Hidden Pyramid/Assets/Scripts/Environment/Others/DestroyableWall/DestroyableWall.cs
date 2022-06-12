using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public float timeToDeactivate = 2f;
    public ExplosiveOrigin explosiveOrigin;

    public Collider [] collidersToDeactivate;
    public Rigidbody [] piecesToExplode;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Bullet"))
        {
            SetupPieces();
            explosiveOrigin.Explode(piecesToExplode);
            StartCoroutine(DeactivateColliders());
        }
    }

    private void SetupPieces()
    {
        foreach (Rigidbody rb in piecesToExplode)
            rb.isKinematic = false;
    }

    private IEnumerator DeactivateColliders()
    {
        yield return new WaitForSeconds(timeToDeactivate);
        foreach (Collider collider in collidersToDeactivate)
            collider.enabled = false;
    }
}
