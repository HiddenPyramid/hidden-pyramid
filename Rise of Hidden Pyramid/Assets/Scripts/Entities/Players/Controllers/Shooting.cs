using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform weaponContainer;

    private IWeapon currentWeapon;
    private PlayerInput input;
    private InputAction shootAction;
    private InputAction aimAction; 
    private Vector3 direction;
    private bool isGamepad = false;

    private bool inverted = true;
    // Start is called before the first frame update

    void Start()
    {
        currentWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
        input = GetComponent<PlayerInput>();
        shootAction = input.actions[Parameter.ACTION_SHOOT];
        aimAction = input.actions[Parameter.ACTION_AIM];
        shootAction.started += x => Shoot(x);
        shootAction.performed += x => Shoot(x);
        shootAction.canceled += x => Shoot(x);
        //aimAction.performed += x => ReadInput(x.ReadValue<Vector2>()); // No aiming anymore
    }

    private void ReadInput(Vector2 vector2)
    {
        Vector3 temp = new Vector3(vector2.x, vector2.y, Camera.main.transform.position.z);
        Vector3 dir = Camera.main.ScreenToWorldPoint(temp) - weaponContainer.position;
        direction = dir;
    }


    /*void Update()
    {
        CheckWeapon(); // Weapon is always the same
        //if (currentWeapon != null) Rotate(); // No rotation anymore
    }*/


    private void Shoot(InputAction.CallbackContext callback)
    {
        if(currentWeapon != null)
            currentWeapon.Attack(inverted);
    }

    /* private void CheckWeapon()
    {
        currentWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
    }

    
    private void Rotate()
    {
        weaponContainer.rotation = Quaternion.LookRotation(weaponContainer.forward, direction);
        weaponContainer.rotation *= Quaternion.Euler(0, 0, 90);
    } */

    /* public void OnDeviceChange(PlayerInput playerInput) // Device is always the same
    {
        isGamepad = playerInput.currentControlScheme.Equals("Gamepad");
    } */
    
    public void FaceLeft()
    {
        inverted = false;
    }

    public void FaceRight()
    {
        inverted = true;
    }
}
