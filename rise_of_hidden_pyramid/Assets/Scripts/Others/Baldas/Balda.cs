using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balda : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 originalPos;

    public GameObject laser;

    void Start()
    {
        targetPos = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Piso");
            // MoveDown();
            transform.position = targetPos;
            Shoot();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Debug.Log("Dejo de pisar");
        transform.position =  originalPos;

    }

    void Shoot()
    {
        laser.GetComponent<Laser>().Shoot();
    }




    // public void MoveDown()
    // {
    //     while (targetPos != transform.position)
    //     {
    //         transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
    //     }
    // }
}
