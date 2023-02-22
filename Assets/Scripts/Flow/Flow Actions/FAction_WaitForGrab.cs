using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAction_WaitForGrab : FlowAction
{
    public Interactable target;
    bool grabbed;
    public override void Begin()
    {
        base.Begin();
        
        instigator.StartCoroutine(Run());
        target.onGrab.AddListener(GrabHandler);
    }

    public override void Complete()
    {
        base.Complete();
        target.onGrab.RemoveListener(GrabHandler);
    }

    IEnumerator Run ()
    {
        grabbed = false;
        while (!grabbed)
        {
            yield return null;
        }
        Complete();
        yield break;
    }

    void GrabHandler (Interactor interactor)
    {
        grabbed = true;
    }
}
