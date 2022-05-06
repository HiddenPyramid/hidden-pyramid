using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmo : MonoBehaviour
{

    public float speed = 200f;
    public Rigidbody rb;
    public int damage = 100;
    private Transform CameraTransform;

    // public GameObject shootingFirePoint;


    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = Camera.main.transform;
        rb.velocity = transform.right * speed;
        StartCoroutine(AutoDestroy());
    }


    public IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
