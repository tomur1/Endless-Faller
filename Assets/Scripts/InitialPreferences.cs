using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPreferences
{
    public Vector3 initialPlayerPosition;
    public float initialPlatformSpeed;

    public InitialPreferences(Vector3 initialPlayerPosition, float initialPlatformSpeed)
    {
        this.initialPlayerPosition = initialPlayerPosition;
        this.initialPlatformSpeed = initialPlatformSpeed;
    }

}
