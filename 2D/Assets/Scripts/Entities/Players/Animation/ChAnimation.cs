using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator shadowAnimator;
    [SerializeField]
    private Transform visuals;
    [SerializeField]
    private Transform shadowVisuals;
    public Transform slymVisuals;

    private InputAction moveAction;
    private float input;
    private int lastInput;
    private bool startedJumping = false;
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
        if (input > 0)
        {
            animator.SetBool(Parameter.ANIM_RUNNING, true);
            shadowAnimator.SetBool(Parameter.ANIM_RUNNING, true);
            visuals.localScale = Vector3.one;
            shadowVisuals.localScale = Vector3.one;
            slymVisuals.localScale = Vector3.one;
        }
        else if(input < 0)
        {
            animator.SetBool(Parameter.ANIM_RUNNING, true);
            shadowAnimator.SetBool(Parameter.ANIM_RUNNING, true);
            visuals.localScale = new Vector3(-1, visuals.localScale.y, visuals.localScale.z);
            shadowVisuals.localScale = new Vector3(-1, shadowVisuals.localScale.y, shadowVisuals.localScale.z);
            slymVisuals.localScale = new Vector3(-1, slymVisuals.localScale.y, slymVisuals.localScale.z);
        }
        else
        {
            animator.SetBool(Parameter.ANIM_RUNNING, false);
            shadowAnimator.SetBool(Parameter.ANIM_RUNNING, false);
        }
    }

    public void StartJump()
    {
        if (!animator.GetBool(Parameter.ANIM_JUMPING))
        {
            animator.SetTrigger(Parameter.ANIM_TAKEOFF);
            animator.SetBool(Parameter.ANIM_JUMPING, true);
            shadowAnimator.SetBool(Parameter.ANIM_JUMPING, true);
            shadowAnimator.SetTrigger(Parameter.ANIM_TAKEOFF);
        }
        else
        {
            animator.ResetTrigger(Parameter.ANIM_TAKEOFF);
            shadowAnimator.ResetTrigger(Parameter.ANIM_TAKEOFF);
        }
    }

    public void EndJump()
    {
        if (animator.GetBool(Parameter.ANIM_JUMPING))
        {
            animator.SetBool(Parameter.ANIM_JUMPING, false);
            shadowAnimator.SetBool(Parameter.ANIM_JUMPING, false);
        }
    }
}
