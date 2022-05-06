using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{
    public static Action<bool> Pickable;
    public static Action InWall;
    public static Action InAir;
    public static Action Landing;
    public static Action Jumping;
    public static Action Died;
    public static Action Bored;
    public static Action<float> ButtonPressed;
    public static Action<float> KeyGrab;
    public static Action<bool> Gliding;

}
