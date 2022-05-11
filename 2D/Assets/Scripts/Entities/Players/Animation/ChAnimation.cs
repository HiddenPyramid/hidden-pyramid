using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private InputAction moveAction;
    private float input;
    // Start is called before the first frame update
    void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions[Parameter.ACTION_MOVE];
        moveAction.performed += x => input = x.ReadValue<Vector2>().x;
        moveAction.canceled += _ => input = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (input != 0)
            animator.SetBool(Parameter.ANIM_RUNNING, true);
        else
            animator.SetBool(Parameter.ANIM_RUNNING, false);
    }
}
