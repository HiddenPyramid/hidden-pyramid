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
    public Arm arm;
    public Head head;
    public bool isLeft = true;

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
            StopLaser();
            StopWarning();
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
        if (this.head != null)
        {
            if (isLeft) head.FallLeft();
            else head.FallRight();
        }
            
    }
}
