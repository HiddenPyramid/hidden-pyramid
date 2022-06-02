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

    public float playerDieTime = 5f;
    private bool hasDied = false;
    public Animator curtainAnimator;
    private float timeToCloseCurtain = 3f;

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
        playerAnimator.SetTrigger(Parameter.ANIM_DIES);
        shadowPlayerAnimator.SetTrigger(Parameter.ANIM_DIES);

        yield return new WaitForSeconds(playerDieTime);

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
    }

}
