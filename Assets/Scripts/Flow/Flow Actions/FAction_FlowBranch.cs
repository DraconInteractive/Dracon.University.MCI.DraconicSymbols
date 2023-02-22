using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAction_FlowBranch : FlowAction
{
    public FlowCondition condition;

    public List<FlowHost> targets;

    public override void Begin()
    {
        base.Begin();

        int i = condition.Evaluate();
        targets[i].Run();
        Complete();
    }
}
