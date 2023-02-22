using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FAction_MoveToTransform : FlowAction
{
    public Transform target;
    public Transform moveTo;

    [SuffixLabel("- use 0 to snap immediately")]
    public float duration;
    public override void Begin()
    {
        base.Begin();

        if (duration == 0)
        {
            target.position = moveTo.position;
            Complete();
        }
        else
        {
            instigator.StartCoroutine(Run());
        }
    }

    IEnumerator Run ()
    {
        Vector3 startPosition = target.position;
        for (float f = 0; f < 1; f += Time.deltaTime / duration)
        {
            target.position = Vector3.Lerp(startPosition, moveTo.position, f);
            yield return null;
        }
        Complete();
    }
}
