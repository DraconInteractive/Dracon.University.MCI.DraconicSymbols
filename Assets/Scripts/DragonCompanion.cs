using Dracon.Core;
using Dracon.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonCompanion : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    public Animator animator;
    public Animator uiAnimator;
    public NavMeshAgent agent;

    [Header("Movement")]
    public float movementThreshold;
    public float stopThreshold;

    [Header("UI")]
    public TMPro.TMP_Text wealthText, timeText;

    public enum State
    {
        Idle, 
        MovingToPlayer,
        InteractingWithPlayer
    }

    [Header("Debug")]
    public State state;
    public float distanceToPlayer;

    [ContextMenu("Setup")]
    public void Setup ()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = ReferenceManager.Instance.playerHead;
        agent.stoppingDistance = stopThreshold;
        SetState(State.Idle);
        StartCoroutine(MovementRoutine());
    }

    IEnumerator MovementRoutine ()
    {
        while (true)
        {
            float dist = PlayerDistance();
            if (dist > movementThreshold && state == State.Idle)
            {
                agent.SetDestination(player.transform.position);
                agent.isStopped = false;
                SetState(State.MovingToPlayer);
                while (dist > stopThreshold * 1.01f)
                {
                    dist = PlayerDistance();
                    distanceToPlayer = dist;
                    agent.SetDestination(player.transform.position);
                    yield return new WaitForSeconds(0.1f);
                }
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                SetState(State.Idle);
            }
            yield return null;
        }
    }

    void SetState (State newState)
    {
        state = newState;
    }

    float PlayerDistance ()
    {
        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, playerPos);
        distanceToPlayer = dist;
        return dist;
    }

    [ContextMenu("Open UI")]
    public void OpenUI ()
    {
        uiAnimator.SetBool("Open", true);
    }

    [ContextMenu("Close UI")]
    public void CloseUI ()
    {
        uiAnimator.SetBool("Open", false);
    }

    public void UpdateUI ()
    {
        wealthText.text = "Wealth: " + WealthManager.Instance.currentWealth;
        timeText.text = "0:00";
    }
}
