using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DragonClaws : MonoBehaviour
{
    public static bool Active;

    [ReadOnly] public bool debug;
    private void OnEnable()
    {
        Toggle(true);
    }

    public void Toggle()
    {
        Toggle(!Active);
    }

    public void Toggle(bool state)
    {
        Active = state;
        debug = state;
    }
}
