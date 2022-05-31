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
        if (!canShoot || !this.gameObject.activeSelf) return;
        Instantiate(bullet, transform.position, transform.rotation, null);
        StartCoroutine(Delay());
    }

    public override void Attack(bool inverted)
    {
        if (!canShoot || !this.gameObject.activeSelf) return;

        if (inverted) Instantiate(bullet, transform.position, transform.rotation, null);
        else {
            Bullet instantiatedBullet = Instantiate(bullet, transform.position,  new Quaternion(0,0,0,-1), null).GetComponent<Bullet>();
            instantiatedBullet.inverted = true;
        }
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        canShoot = false;
        yield return new WaitForSeconds(rate);
        canShoot = true;
    }
}
