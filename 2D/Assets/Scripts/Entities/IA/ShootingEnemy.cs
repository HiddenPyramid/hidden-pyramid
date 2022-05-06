using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{

    public int health = 100;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int arms;

    // public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking damage!");
        health -= damage;

        if (health <= 0)
            Die();
    }


    void Die()
    {
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    void Hit()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);

        if (collider.gameObject.CompareTag("Player"))
        {

            // collider.gameObject.GetComponent<>().takeDamage();

        }
    }



}
