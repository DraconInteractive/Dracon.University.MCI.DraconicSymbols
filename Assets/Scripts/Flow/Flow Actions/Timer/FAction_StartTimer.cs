using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAction_StartTimer : FlowAction
{
    public ScenarioTimer timer;
    public bool useTimerSettings;
    [HideIf("useTimerSettings")]
    public bool overrideCurrent;
    [HideIf("useTimerSettings")]
    public float duration;

    public override void Begin()
    {
        base.Begin();
        if (useTimerSettings)
        {
            timer.Begin(false);
        }
        else
        {
            timer.Begin(overrideCurrent, duration);
        }
        Complete();
    }
}
