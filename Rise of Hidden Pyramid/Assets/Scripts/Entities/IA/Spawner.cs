using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float rate;
    [SerializeField]
    private Transform prefab;

    private PlayerDetection detection;
    private bool spawn = true;
    // Start is called before the first frame update
    void Start()
    {
        detection = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detection.PlayersDetected.Count > 0 && spawn)
        {

            Transform temp = Instantiate(prefab, transform);
            temp.position = transform.position;
            temp.rotation = transform.rotation;
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        spawn = false;
        yield return new WaitForSeconds(rate);
        spawn = true;
    }
}
