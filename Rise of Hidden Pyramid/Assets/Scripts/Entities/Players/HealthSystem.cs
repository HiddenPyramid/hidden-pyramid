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
    public Animator playerAnimator, slymAnimator;
    private PlayerController playerController;

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
        if(player.Health <= 0)
        {
            transform.position = Respawn;
            transform.rotation = Quaternion.identity;
            Camera.main.transform.rotation = transform.rotation;
            player.Health = Lives;
            
            playerAnimator.SetTrigger(Parameter.ANIM_DIES);
            slymAnimator.SetTrigger(Parameter.ANIM_DIES);

            playerController.RegainLives();
        }
    }
    //private PlayerStats player;
    //private PlayerController controller;
    //private HealthSystem partner;
    //private InputAction reviveAction;
    //private bool dead = false;

    //public bool Dead { get => dead; set => dead = value; }
    //public HealthSystem Partner { get => partner; set => partner = value; }
    // Start is called before the first frame update
    //void Start()
    //{
    //    //player = GetComponent<PlayerStats>();
    //    //controller = GetComponent<PlayerController>();
    //    //reviveAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_REVIVE];
    //    //reviveAction.started += _ => SetReviving();
    //    //reviveAction.performed += _ => RevivePartner();
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if(player.Health <= 0)
    //        Die();
    //}

    //public void Revive()
    //{
    //    dead = false;
    //    controller.enabled = true;
    //}

    //private void Die()
    //{
    //    dead = true;
    //    controller.enabled = false;
    //}
    //private void RevivePartner()
    //{
    //    if (partner == null) return;
    //    partner.Revive();
    //    controller.enabled = true;
    //}
    //private void SetReviving()
    //{
    //    if (partner == null) return;
    //    if (partner.Dead)
    //    {
    //        controller.enabled = false;
    //    }
    //}

}