using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private Transform Respawn;
    [SerializeField]
    private int Lives;

    private PlayerStats player;
    public Animator playerAnimator, slymAnimator, shadowPlayerAnimator;
    private PlayerController playerController;

    public float playerDieTime = 4.2f;
    private bool hasDied = false;
    public Animator curtainAnimator;
    public Animator keepOnAnimator;
    private float timeToCloseCurtain = 3f;

    public ParticleSystem particleSystem;

    public GameObject[] spawnLevels;
    public static int spawnLevel;

    public NoDamageOnDie noDamageOnDie;
    public TrapRespawner optionalTrapRespawner;
    

    public bool die = false;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        CheckDead();

        if (die) { die = false; PlayerStats.Health = 0; }// EXTRA DEBUGGING
    }

    private void CheckDead()
    {
        if(PlayerStats.Health <= 0 && !hasDied)
        {
            hasDied = true;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {        
        noDamageOnDie.StopReceivingDamage();

        SolveLevelIndexOnBossBattle();
        spawnLevels[spawnLevel].SetActive(true);
        if (optionalTrapRespawner!= null) optionalTrapRespawner.RespawnTrap();
        GetComponent<PlayerMovement>().BlockMove();

        playerAnimator.SetTrigger(Parameter.ANIM_DIES);
        shadowPlayerAnimator.SetTrigger("dieAndDisappear");

        yield return new WaitForSeconds(playerDieTime/2);
        
        if (particleSystem != null)
        {
            Instantiate(particleSystem, transform.position, transform.rotation, null);
        }
        
        curtainAnimator.SetTrigger(Parameter.ANIM_REVIVES);
        keepOnAnimator.gameObject.SetActive(true);
        keepOnAnimator.SetBool("respawning", true);

        yield return new WaitForSeconds(timeToCloseCurtain);
        keepOnAnimator.SetBool("respawning", false);

        playerAnimator.SetTrigger(Parameter.ANIM_REVIVES);
        shadowPlayerAnimator.SetTrigger(Parameter.ANIM_REVIVES);

        transform.position = Respawn.position;
        //transform.rotation = Quaternion.identity;
        //Camera.main.transform.rotation = transform.rotation;
        PlayerStats.Health = Lives;
            
        playerController.RegainLives();
        hasDied = false;
        GetComponent<PlayerMovement>().UnblockMove();
        
        yield return new WaitForSeconds(1f);
        keepOnAnimator.gameObject.SetActive(false);

        noDamageOnDie.RestartReceivingDamage();
    }

    private void SolveLevelIndexOnBossBattle()
    {
        if (spawnLevels.Length < spawnLevel+1)
        {
            spawnLevel = 2;
        }
    }

    public float GetHealth()
    {
        return PlayerStats.Health;
    }

    public void InstaKill()
    {
        playerController.TakeDamageInstaKill(3);
    }
}
