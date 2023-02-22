using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BaseAction
{
    protected MonoBehaviour owner;

    public delegate void ActionFunc();
    public ActionFunc onBegin;
    public ActionFunc onCancel;
    public ActionFunc onComplete;
    public ActionFunc onError;

    public enum State
    {
        Idle,
        Running,
        Complete,
        Error,
        Cancelled
    }
    public State state;

    public float tickRate;

    protected Coroutine _routine;

    public virtual void Begin(MonoBehaviour _owner, ref Coroutine _actionRoutine, float _tickRate)
    {
        state = State.Idle;
        owner = _owner;
        tickRate = _tickRate;


        if (onBegin != null)
        {
            onBegin();
        }

        state = State.Running;
        _actionRoutine = owner.StartCoroutine(Run());
        _routine = _actionRoutine;
    }

    public virtual void Cancel()
    {
        state = State.Cancelled;
        if (_routine != null)
        {
            owner.StopCoroutine(_routine);
        }
        if (onCancel != null)
        {
            onCancel();
        }
    }

    public virtual void Complete()
    {
        Debug.Log($"Completed {GetType().Name} action!");
        state = State.Complete;
        if (onComplete != null)
        {
            onComplete();
        }
    }

    public virtual IEnumerator Run()
    {
        yield break;
    }
}

[Serializable]
public class MoveToAction : BaseAction
{
    public NavMeshAgent agent;
    public Vector3 target;
    public float stoppingDistance;
    public bool stopAtEnd = false;

    public override void Cancel()
    {
        agent.isStopped = true;
        base.Cancel();
    }

    public override IEnumerator Run()
    {
        agent.isStopped = false;

        bool proceed = true;
        while (proceed)
        {
            float dist = Vector3.Distance(owner.transform.position, target);
            if (dist < stoppingDistance)
            {
                proceed = false;
            }
            else
            {
                agent.SetDestination(target);
            }

            yield return new WaitForSeconds(tickRate);
        }

        if (stopAtEnd)
        {
            agent.isStopped = true;
        }
        Complete();
    }
}