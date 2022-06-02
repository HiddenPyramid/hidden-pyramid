using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 25.0f;
    public bool inverted = false;
    public float timeToDestroy = 1.0f;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    private void Update()
    {
        if (inverted) { Rotate(); inverted = false; }
        transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        transform.position += transform.right * speed * Time.deltaTime;
    }
    

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    private void Rotate()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x,rot.y+180,rot.z);
        transform.rotation = Quaternion.Euler(rot);
    }

}
