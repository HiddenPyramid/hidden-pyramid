using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform[] spawningCoords;



    void Start()
    {
        StartCoroutine(enemySpawning());
    }


    public IEnumerator enemySpawning()
    {
        yield return new WaitForSeconds(5);

        foreach (Transform t in spawningCoords)
        {
            GameObject enemy = Instantiate(enemyPrefab, t);
            enemy.GetComponent<Rigidbody>().mass = 30;

            yield return new WaitForSeconds(1);
            enemy.GetComponent<Rigidbody>().mass = 1;
        }

    }

    public void StopCoroutine(){
        StopCoroutine(enemySpawning());
    }

}
