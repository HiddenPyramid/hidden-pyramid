using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Vector3 startPos;
    public GameObject finalPosObject;
    private Vector3 finalPos;
    public float speed = 3;

    public GameObject laserObject;
    public GameObject laserShootFrom;



    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        finalPos = finalPosObject.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
        if (transform.position == finalPos)
        {
            var pivot = startPos;
            startPos = finalPos;
            finalPos = pivot;
        }
    }


    public void Shoot()
    {
        Instantiate(laserObject, laserShootFrom.transform.position, laserShootFrom.transform.rotation);
    }


}
