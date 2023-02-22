using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioTimer : MonoBehaviour
{
    [Title("Timer Main")]
    public float elapsed;
    public float timerLength;

    public enum State
    {
        Idle,
        Running,
        Paused,
        Complete,
        Cancelled
    }
    public State state;

    Coroutine runRoutine;

    #region Events
    [Title("Events")]
    public UnityEvent onStart;
    [System.Serializable]
    public class TimerUpdateEvent : UnityEvent<float> {}
    public TimerUpdateEvent onTimerUpdate;
    public UnityEvent onCancel, onPause, onUnPause, onComplete;
    #endregion
    public void Begin (bool overrideCurrentTimer, float duration)
    {
        timerLength = duration;
        Begin(overrideCurrentTimer);
    }

    public void Begin (bool overrideCurrentTimer)
    {
        if (runRoutine != null)
        {
            if (overrideCurrentTimer)
            {
                StopCoroutine(runRoutine);
            }
            else
            {
                return;
            }
        }
        runRoutine = StartCoroutine(Run());
    }

    public void Cancel ()
    {
        if (runRoutine != null)
        {
            StopCoroutine(runRoutine);
        }
        state = State.Cancelled;
        onCancel?.Invoke();
    }

    public void Complete ()
    {
        if (runRoutine != null)
        {
            StopCoroutine(runRoutine);
        }
        state = State.Complete;
        onComplete?.Invoke();
    }

    public void Pause ()
    {
        state = State.Paused;
        onPause?.Invoke();
    }

    public void UnPause ()
    {
        if (runRoutine != null)
        {
            state = State.Running;
        }
        else
        {
            state = State.Idle;
        }
        onUnPause?.Invoke();
    }

    IEnumerator Run ()
    {
        state = State.Running;
        elapsed = 0;
        onStart?.Invoke();

        while (elapsed < timerLength)
        {
            if (state == State.Paused) continue;

            elapsed += Time.deltaTime;
            onTimerUpdate?.Invoke(elapsed);
            yield return null;
        }

        Complete();
        yield break;
    }
}
