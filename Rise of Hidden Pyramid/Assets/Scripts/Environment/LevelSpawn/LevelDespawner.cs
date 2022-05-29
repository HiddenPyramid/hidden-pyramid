using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] 
public class LevelDespawner : MonoBehaviour
{
    // Intended usage: attack to object, make prefab and:
    // (Optional) 1 Place Level Spawner for next level
    // 2 Place Level Spawner for previous level (in case player goes back)
    // 3 Place Level DESpawner for previous level

    public GameObject level;
    
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        if(level.activeSelf) level.SetActive(false);
    }
}
