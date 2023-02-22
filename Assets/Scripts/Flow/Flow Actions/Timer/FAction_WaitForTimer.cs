using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAction_WaitForTimer : FlowAction
{
    public ScenarioTimer timer;

    public override void Begin()
    {
        base.Begin();
        instigator.StartCoroutine(Wait());
    }

    IEnumerator Wait ()
    {
        while (timer.state == ScenarioTimer.State.Running || timer.state == ScenarioTimer.State.Paused)
        {
            yield return null;
        }
        Complete();
        yield break;
    }
}
