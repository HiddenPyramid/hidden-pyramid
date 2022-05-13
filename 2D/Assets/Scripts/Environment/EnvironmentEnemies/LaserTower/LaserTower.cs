using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour, ITakeDamage
{
    public LaserRay laserRay;
    public WarningLaser warningLaser;
    public Animator animator;
    private bool isActive = true;
    public float laserInterval = 10f;
    public float warningInterval = 5f;
    public GoBackTrigger goBackTrigger;

    public float timeToDie = 2.0f;
    public float initialHP = 50;

    [SerializeField]
    protected float Health;
    public Arm arm;

    private void Start() 
    {
        StartCoroutine(ShootingLoop());
        this.Health = initialHP;
    }

    private void Update() 
    {
        CheckHealth();
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


    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }
    protected bool CheckDie()
    {
        if (Health <= 0)
            return true;
        return false;
    }

    private void CheckHealth()
    {
        if (CheckDie())
        {
            animator.SetTrigger("die");
            //StartCoroutine(WaitToDestroy(timeToDie));
            this.isActive = false;
            CheckBossWarning();
        }
    }

    private IEnumerator WaitToDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void CheckBossWarning()
    {
        if (this.arm != null)
            this.arm.Fall();
    }
}
