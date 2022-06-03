using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private Vector3 Respawn;
    [SerializeField]
    private int Lives;

    private PlayerStats player;
    public Animator playerAnimator, slymAnimator, shadowPlayerAnimator;
    private PlayerController playerController;

    public float playerDieTime = 4.2f;
    private bool hasDied = false;
    public Animator curtainAnimator;
    private float timeToCloseCurtain = 3f;

    public ParticleSystem particleSystem;

    public GameObject[] spawnLevels;
    public static int spawnLevel;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        CheckDead();
        Debug.Log(transform.position);
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
        spawnLevels[spawnLevel].SetActive(true);
        spawnLevel ++;
        GetComponent<PlayerMovement>().BlockMove();

        playerAnimator.SetTrigger(Parameter.ANIM_DIES);
        shadowPlayerAnimator.SetTrigger("dieAndDisappear");

        yield return new WaitForSeconds(playerDieTime/2);
        
        if (particleSystem != null)
        {
            Instantiate(particleSystem, transform.position, transform.rotation, null);
        }

        yield return new WaitForSeconds(playerDieTime/2);

        curtainAnimator.SetTrigger(Parameter.ANIM_REVIVES);

        yield return new WaitForSeconds(timeToCloseCurtain);

        playerAnimator.SetTrigger(Parameter.ANIM_REVIVES);
        shadowPlayerAnimator.SetTrigger(Parameter.ANIM_REVIVES);

        transform.position = Respawn;
        //transform.rotation = Quaternion.identity;
        //Camera.main.transform.rotation = transform.rotation;
        PlayerStats.Health = Lives;
            
        

        playerController.RegainLives();
        hasDied = false;
        GetComponent<PlayerMovement>().UnblockMove();
    }

}
