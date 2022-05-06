using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Picking : MonoBehaviour
{
    public Transform pickTransform;

    private Transform pickable;
    private InputAction pickAction;
    private bool slotFull = false;

    public Transform Pickable { get => pickable; set => pickable = value; }
    // Start is called before the first frame update
    void Start()
    {
        pickable = null;
        pickAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_PICK];
        pickAction.performed += _ => Pick();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Pick()
    {
        if (pickable == null) return;
        if (!slotFull)
        {
            slotFull = true;
            pickable.transform.SetParent(pickTransform);
            pickable.GetComponent<IWeapon>().PickUp();
        }
        else
        {
            slotFull = false;
            pickable.transform.SetParent(null);
            pickable.GetComponent<IWeapon>().Drop();
            pickable = null;
        }
    }
    
}
