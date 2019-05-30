using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    public static bool paused = false;
    public static float deltaTime
    {
        get
        {
            return paused ? 0 : Time.deltaTime;
        }
    }
}
