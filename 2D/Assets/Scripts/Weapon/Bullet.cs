using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 25.0f;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    private void Update()
    {
        //transform.Translate(transform.up * speed * Time.deltaTime);
        transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        //transform.position += transform.up * speed * Time.deltaTime;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
