using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : Weapon
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float rate;
    private bool canShoot = true;
    public override void Attack(InputAction.CallbackContext callback)
    {

        if (!canShoot) return;
        Instantiate(bullet, transform.position, transform.rotation, null);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        canShoot = false;
        yield return new WaitForSeconds(rate);
        canShoot = true;
    }
}
