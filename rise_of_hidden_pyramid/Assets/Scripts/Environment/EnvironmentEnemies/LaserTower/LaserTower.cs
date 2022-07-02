using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour, ITakeDamage
{
    public ParticleSystem laserParticles;
    public ParticleSystem warningParticles;

    public LaserRay laserRay;
    //public WarningLaser warningLaser;
    public Animator animator;
    private bool isActive = true;

    public float pauseDuration = 4f;
    public float warningDuration = 3f;
    public float laserDuration = 1f;

    public GoBackTrigger goBackTrigger;

    public float timeToDie = 2.0f;
    public float initialHP = 50;

    [SerializeField]
    protected float Health;
    public Arm armIfArmTower;
    public Head headIfCheekTower;
    public bool isLeftIfCheekTower = true;

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
        StopLaser();
        StopWarning();
        while (isActive)
        {
            yield return new WaitForSeconds(pauseDuration);
            //goBackTrigger.gameObject.SetActive(false);
            ShootWarning();
            yield return new WaitForSeconds(warningDuration);
            //goBackTrigger.gameObject.SetActive(true);
            StopWarning();
            ShootLaser();
            yield return new WaitForSeconds(laserDuration);
            StopLaser();
        }
    }
    private void ShootLaser()
    {
        this.animator.SetTrigger("shoot");
        this.laserRay.Shoot();
        this.laserParticles.Play();
    }

    private void StopLaser()
    {
        this.laserRay.Stop();
        this.laserParticles.Stop();
    }

    private void StopCompletelyLaser()
    {
        this.laserRay.Defeated();
        StopLaser();
    }

    private void ShootWarning()
    {
        this.warningParticles.Play();
    }

    private void StopWarning()
    {
        this.warningParticles.Stop();
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
            StopCompletelyLaser();
            laserRay.gameObject.SetActive(false);
            StopWarning();
            animator.SetTrigger("die");
            this.isActive = false;
            CheckBossWarning();
            this.enabled = false;
        }
    }

    private void CheckBossWarning()
    {
        if (this.armIfArmTower != null)
            this.armIfArmTower.Fall();
        if (this.headIfCheekTower != null)
        {
            if (isLeftIfCheekTower) headIfCheekTower.FallLeft();
            else headIfCheekTower.FallRight();
        }
            
    }
}
