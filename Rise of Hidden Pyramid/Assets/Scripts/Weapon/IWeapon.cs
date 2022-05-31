using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IWeapon 
{
    void Attack(InputAction.CallbackContext callback);
    void Attack(bool inverted);
    void PickUp();
    void Drop();

}
