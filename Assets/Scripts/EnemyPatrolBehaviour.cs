using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : EnemyBehaviour
{
    public bool snapToStart;
    public int currentIndex;
    public PathNode[] pathNodes;

    public enum State
    {
        Idle,
        Running, 
        Paused,
        Complete
    }
    public State state;

    public override void Setup(BaseEnemy _owner)
    {
        base.Setup(_owner);
        //ExecutePatrol();
        Invoke("ExecutePatrol", 0.5f);
    }

    public override void Tick()
    {
        base.Tick();
    }

    [Button("Execute Patrol")]
    public void ExecutePatrol()
    {
        StartCoroutine(PatrolRoutine());
    }

    public void Pause ()
    {

    }

    IEnumerator PatrolRoutine ()
    {
        state = State.Running;
        bool doLoop = true;

        while (doLoop)
        {
            foreach (var node in pathNodes)
            {
                var moveAction = new MoveToAction()
                {
                    agent = owner.agent,
                    target = node.transform.position,
                    stoppingDistance = 0.2f,
                    stopAtEnd = false
                };
                owner.StartAction(moveAction, 0);
                while (moveAction.state == BaseAction.State.Running)
                {
                    yield return null;
                }

                foreach (var action in node.actions)
                {
                    owner.StartAction(action, 0);
                    while (action.state == BaseAction.State.Running)
                    {
                        yield return null;
                    }
                }

                yield return null;
            }
            yield return null;
        }
        yield break;
    }

    private void OnDrawGizmosSelected()
    {
        if (pathNodes != null && pathNodes.Length > 0)
        {
            Vector3 last = transform.position;
            Gizmos.color = Color.red;

            foreach (var point in pathNodes)
            {
                Gizmos.DrawLine(last, point.transform.position);
                last = point.transform.position;
            }
        }
    }
}
