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
    public Animator keepOnAnimator;
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
        FindObjectOfType<PlayerManager>().isAlive = false;

        spawnLevels[spawnLevel].SetActive(true);
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

        transform.position = Respawn;
        //transform.rotation = Quaternion.identity;
        //Camera.main.transform.rotation = transform.rotation;
        PlayerStats.Health = Lives;
            
        playerController.RegainLives();
        hasDied = false;
        GetComponent<PlayerMovement>().UnblockMove();
        
        yield return new WaitForSeconds(1f);
        keepOnAnimator.gameObject.SetActive(false);

        FindObjectOfType<PlayerManager>().isAlive = true;
    }

    public float GetHealth()
    {
        Debug.Log(PlayerStats.Health);
        return PlayerStats.Health;
    }
}
