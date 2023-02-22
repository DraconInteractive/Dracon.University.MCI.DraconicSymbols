using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
public class DragonEyes : MonoBehaviour
{
    public static bool dragonEyesActive;
    [ReadOnly] 
    public bool debug;
    private void Start()
    {
        ToggleDragonEyes(false);
    }

    public void ToggleDragonEyes()
    {
        dragonEyesActive = !dragonEyesActive;
        ToggleDragonEyes(dragonEyesActive);
    }

    public void ToggleDragonEyes(bool state)
    {
        dragonEyesActive = state;
        debug = state;
        DragonOutline.All.ForEach(x => x.Toggle(state));
    }
}
