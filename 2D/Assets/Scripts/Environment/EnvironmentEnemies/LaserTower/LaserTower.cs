using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{
    public LaserRay laserRay;
    public WarningLaser warningLaser;
    public Animator animator;
    private bool isActive = true;
    public float laserInterval = 10f;
    public float warningInterval = 5f;
    public GoBackTrigger goBackTrigger;

    private void Start() 
    {
        StartCoroutine(ShootingLoop());
    }

    private IEnumerator ShootingLoop()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(warningInterval);
            //goBackTrigger.gameObject.SetActive(false);
            ShootWarning();
            yield return new WaitForSeconds(laserInterval);
            //goBackTrigger.gameObject.SetActive(true);
            ShootLaser();
        }
    }
    private void ShootLaser()
    {
        this.animator.SetTrigger("shoot");
        this.laserRay.Shoot();
    }

    private void ShootWarning()
    {
        this.warningLaser.Shoot();
    }
}
