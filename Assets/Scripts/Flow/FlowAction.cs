using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FlowAction
{
    public enum State
    {
        Idle,
        Running, 
        Complete,
        Cancelled
    }
    public State state;

    protected FlowHost instigator;

    public virtual void Setup (FlowHost _instigator)
    {
        instigator = _instigator;
    }

    public virtual void Begin ()
    {
        state = State.Running;
    }

    public virtual void Complete ()
    {
        state = State.Complete;
    }

    public virtual void Reset ()
    {
        state = State.Idle;
    }

    public virtual void Cancel ()
    {
        state = State.Cancelled;
    }
}
