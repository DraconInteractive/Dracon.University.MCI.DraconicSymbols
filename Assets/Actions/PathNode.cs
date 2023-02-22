using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathNode : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    public List<PathAction> actions = new List<PathAction>();

}

[Serializable]
public class PathAction : BaseAction
{

}

[AddTypeMenu("Path/Stop Movement")]
[Serializable]
public class PathStopMoveAction : PathAction
{
    public NavMeshAgent agent;
    public override IEnumerator Run()
    {
        agent.isStopped = true;
        Complete();
        yield break;
    }
}

[AddTypeMenu("Path/Wait For Seconds")]
[Serializable]
public class PathPauseAction : PathAction
{
    public float duration;

    public override IEnumerator Run()
    {
        yield return new WaitForSeconds(duration);
        Complete();
        yield break;
    }
}

[AddTypeMenu("Path/Wait For Seconds - Random")]
[Serializable]
public class PathPauseRandomAction : PathAction
{
    public float minDuration, maxDuration;

    public override IEnumerator Run()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minDuration, maxDuration));
        Complete();
        yield break;
    }
}

[AddTypeMenu("Path/Animation Trigger")]
[Serializable]
public class PathAnimationTriggerAction : PathAction
{
    public Animator anim;
    public string trigger;

    public override IEnumerator Run()
    {
        anim.SetTrigger(trigger);
        Complete();
        yield break;
    }
}

[AddTypeMenu("Path/Face Direction")]
[Serializable]
public class PathFaceDirAction : PathAction
{
    public float duration;
    public Transform dir;

    public override IEnumerator Run()
    {
        Quaternion startRot = owner.transform.rotation;
        Quaternion endRot = Quaternion.LookRotation(dir.forward, Vector3.up);

        for (float f = 0; f < 1; f += Time.deltaTime / duration)
        {
            owner.transform.rotation = Quaternion.Lerp(startRot, endRot, f);
            yield return null;
        }

        Complete();
        yield break;
    }
}
