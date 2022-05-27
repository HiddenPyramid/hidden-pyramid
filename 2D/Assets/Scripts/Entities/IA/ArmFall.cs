using System.Collections;
using UnityEngine;

public class ArmFall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody;
    
    public void Drop()
    {
        Debug.Log("Dropeja");
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