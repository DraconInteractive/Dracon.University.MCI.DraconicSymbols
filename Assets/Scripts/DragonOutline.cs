using System;
using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
public class DragonOutline : MonoBehaviour
{
    public static List<DragonOutline> All = new List<DragonOutline>();
    [ReadOnly] public Outlinable outline;

    private void OnValidate()
    {
        if (outline == null)
        {
            outline = GetComponent<Outlinable>();
        }
    }

    private void OnEnable()
    {
        All.Add(this);
        Toggle(DragonEyes.dragonEyesActive);
    }

    private void OnDisable()
    {
        All.Remove(this);
    }

    public void Toggle(bool state)
    {
        if (state)
        {
            ToggleOn();
        }
        else
        {
            ToggleOff();
        }
    }

    public void ToggleOn()
    {
        outline.enabled = true;
    }

    public void ToggleOff()
    {
        outline.enabled = false;
    }
}
