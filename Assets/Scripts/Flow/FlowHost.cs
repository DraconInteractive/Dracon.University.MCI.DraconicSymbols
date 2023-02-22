using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowHost : SerializedMonoBehaviour
{
    public enum Trigger
    {
        Manual,
        OnAwake,
        OnStart
    }

    public enum State
    {
        Idle,
        Running, 
        Complete,
        Cancelled
    }

    public Trigger startTrigger;
    public State state;
    public bool loop;

    [OdinSerialize, System.NonSerialized]
    public FlowAction[] flowActions;
    
    Coroutine runRoutine;
    
    private void Awake()
    {
        Setup();

        if (startTrigger == Trigger.OnAwake)
        {
            Run();
        }
    }

    private void Start()
    {
        if (startTrigger == Trigger.OnStart)
        {
            Run();
        }
    }

    void Setup ()
    {
        foreach (var action in flowActions)
        {
            action.Setup(this);
        }
    }

    public void Run ()
    {
        if (runRoutine != null)
        {
            StopCoroutine(runRoutine);
        }
        runRoutine = StartCoroutine(RunRoutine());
    }

    public void Stop ()
    {
        state = State.Cancelled;
    }

    IEnumerator RunRoutine ()
    {
        state = State.Running;
        foreach (var action in flowActions)
        {
            action.Begin();
            while (action.state != FlowAction.State.Complete)
            {
                if (state == State.Cancelled)
                {
                    action.Cancel();
                    yield break;
                }
                yield return null;
            }
        }
        if (state == State.Cancelled)
        {
            yield break;
        }
        Complete();
        yield break;
    }

    void Complete ()
    {
        state = State.Complete;
        runRoutine = null;
        if (loop)
        {
            Run();
        }
    }

    public void ResetState ()
    {
        state = State.Idle;
    }
}
