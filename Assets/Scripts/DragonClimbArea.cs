using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragonClimbArea : ClimbArea
{
    public override float Evaluate(Vector3 _in)
    {
        if (DragonClaws.Active)
        {
            return base.Evaluate(_in);
        }
        else
        {
            return Mathf.Infinity;
        }
    }
}
