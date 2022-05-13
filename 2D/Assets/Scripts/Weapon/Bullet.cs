using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 25.0f;

    private DamageDealer damageDealer;

    private void Start()
    {
        damageDealer = GetComponent<DamageDealer>();
        StartCoroutine(DestroyBullet());
    }
    private void Update()
    {
        //transform.Translate(transform.up * speed * Time.deltaTime);
        //transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        transform.position += transform.right * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == Parameter.LAYER_ENEMY)
            damageDealer.DealDamage(collision.transform);
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
