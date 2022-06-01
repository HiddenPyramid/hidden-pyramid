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
    private PlayerAndSlym playerAndSlym;

    private void Start()
    {
        playerAndSlym = GetComponentInParent<PlayerAndSlym>();
    }
    public override void Attack(InputAction.CallbackContext callback)
    {
        if (!canShoot || !this.gameObject.activeSelf || !playerAndSlym.shoots) return;
        Instantiate(bullet, transform.position, transform.rotation, null);
        if (playerAndSlym.shoots) StartCoroutine(Delay());
    }

    public override void Attack(bool inverted)
    {
        Debug.Log(playerAndSlym.shoots);
        if (!canShoot || !this.gameObject.activeSelf || !playerAndSlym.shoots) return;

        if (inverted) Instantiate(bullet, transform.position, transform.rotation, null);
        else {
            Bullet instantiatedBullet = Instantiate(bullet, transform.position, transform.rotation * GetQuaternion(), null).GetComponent<Bullet>();
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

    private Quaternion GetQuaternion()
    {
        switch (playerAndSlym.axisToEnforce2DMovement)
        {
            case Player2DMovementEnforcer.Axis.x:
                Debug.Log("AAAAAAAAAAAAAAAAAA");
                return new Quaternion(0,0,0,-1);
            case Player2DMovementEnforcer.Axis.z:
                Debug.Log("EEEEEEEEEEEEEEEEEEE");
                return new Quaternion(0,0,0,-1);
            default:
                Debug.Log("DEFAULT");
                return new Quaternion(0,0,0,-1);
        }
    }
}
