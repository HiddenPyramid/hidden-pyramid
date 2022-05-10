using System.Collections;
using UnityEngine;

public class ArmFall : MonoBehaviour
{
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Drop()
    {
        transform.SetParent(null);
        rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == Parameter.LAYER_GROUND)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}