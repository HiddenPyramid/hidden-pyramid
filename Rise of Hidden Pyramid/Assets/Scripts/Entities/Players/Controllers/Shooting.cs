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
        aimAction.performed += x => ReadInput(x.ReadValue<Vector2>());
    }

    private void ReadInput(Vector2 vector2)
    {
        if (isGamepad)
        {
            //Vector3 direction = Vector3.up * looking.x + transform.right * looking.y;
            //weaponContainer.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        else
        {
            Vector3 temp = new Vector3(vector2.x, vector2.y, Camera.main.transform.position.z);
            Vector3 dir = Camera.main.ScreenToWorldPoint(temp) - weaponContainer.position;
            direction = dir;
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckWeapon();
        if (currentWeapon != null)
            Rotate();
    }


    private void Shoot(InputAction.CallbackContext callback)
    {
        if(currentWeapon != null)
            currentWeapon.Attack(callback);
    }

    private void CheckWeapon()
    {
        currentWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
    }

    
    private void Rotate()
    {
        weaponContainer.rotation = Quaternion.LookRotation(weaponContainer.forward, direction);
        weaponContainer.rotation *= Quaternion.Euler(0, 0, 90);

    }

    public void OnDeviceChange(PlayerInput playerInput)
    {
        isGamepad = playerInput.currentControlScheme.Equals("Gamepad");
    }
    
}
