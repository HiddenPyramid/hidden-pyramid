using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public float timeToDeactivate = 2f;
    public ExplosiveOrigin explosiveOrigin;

    public Collider [] collidersToDeactivate;
    public Rigidbody [] piecesToExplode;

    public ParticleSystem particlesOnDestroy;
    public AudioSource audioOnDestroy;
    public bool isDestroyed = false;

    private void OnTriggerEnter(Collider other) {
        if (!isDestroyed && other.gameObject.CompareTag("Bullet"))
        {
            isDestroyed = true;
            SetupPieces();
            
            explosiveOrigin.Explode(piecesToExplode);
            particlesOnDestroy.Play();
            audioOnDestroy.Play();

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
