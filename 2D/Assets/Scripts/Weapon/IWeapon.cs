using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IWeapon 
{
    void Attack(InputAction.CallbackContext callback);
    void PickUp();
    void Drop();

}
