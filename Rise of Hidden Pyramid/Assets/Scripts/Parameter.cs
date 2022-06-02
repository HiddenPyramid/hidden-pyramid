using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Parameter
{
    //Input
    public static readonly String ACTION_SHOOT = "Shoot";
    public static readonly String ACTION_AIM = "Look";
    public static readonly String ACTION_PICK = "Pick";
    public static readonly String ACTION_REVIVE = "Revive";
    public static readonly String ACTION_JUMP = "Jump";
    public static readonly String ACTION_MOVE = "Move";
    public static readonly String ACTION_PAUSE = "Pause";

    //Layers
    public const int LAYER_ITEM = 7;
    public const int LAYER_PLAYER = 6;
    public const int LAYER_TRIGGER = 8;
    public const int LAYER_ENEMY = 9;
    public const int LAYER_GROUND = 0;

    //Animation
    public static readonly String ANIM_RUNNING = "isRunning";
    public static readonly String ANIM_JUMPING = "isJumping";
    public static readonly String ANIM_DIES = "dies";
    public static readonly String ANIM_TAKEOFF = "takeOf";

    //Tags
    public static readonly String PLAYER = "Player";
}
