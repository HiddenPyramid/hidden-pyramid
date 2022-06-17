using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{
    public static Action<bool> Pickable;
    public static Action Running;
    public static Action Landing;
    public static Action Jumping;
    public static Action Died;

}
