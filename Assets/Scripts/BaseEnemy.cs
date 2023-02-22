using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public static List<BaseEnemy> All = new List<BaseEnemy>();

    [Header("Animation")]
    public Animator anim;

    [Header("Navigation")]
    public NavMeshAgent agent;
    public float stoppingDistance = 0.1f;

    [Header("Detection")]
    public float lastIncrement;
    public float detectionRate = 0.1f;
    [Space, Range(0.0f, 1.0f)]
    public float detectionThreshold;
    [Range(0.0f, 1.0f)]
    public float detectionLossThreshold;
    [Space]
    public float detectionDecay;
    [Space]
    public EnemyHearing hearing;
    public EnemySight sight;
    public float hearingWeight, sightWeight;

    private float _currentDetection;
    public float CurrentDetection
    {
        get
        {
            return _currentDetection;
        }

        private set
        {
            _currentDetection = Mathf.Clamp01(value);
        }
    }
    public bool detectedPlayer { get; private set; }

    [Header("Beheviour")]
    public EnemyBehaviour behaviour;

    Coroutine actionRoutine;

    public BaseAction currentAction;

    private void Start()
    {
        behaviour.Setup(this);
        CurrentDetection = 0;
        StartCoroutine(DetectionLoop());
    }

#if UNITY_EDITOR
    [ContextMenu("Setup")]
    public void EditorSetup ()
    {
        if (behaviour == null)
        {
            behaviour = GetComponent<EnemyBehaviour>();
        }
        if (hearing == null)
        {
            hearing = GetComponent<EnemyHearing>();
        }
        if (sight == null)
        {
            sight = GetComponent<EnemySight>();
        }
    }
#endif

    private void OnEnable()
    {
        All.Add(this);
    }

    private void OnDisable()
    {
        All.Remove(this);
    }

    private void Update()
    {
        Vector3 velocity = agent.velocity;
        anim.SetFloat("Forward", (velocity.magnitude / agent.speed));
        //anim.SetFloat("Turn", Vector3.Dot(transform.forward, velocity.normalized));
    }

    #region Player Detection
    IEnumerator DetectionLoop ()
    {
        yield return null;
        while (true)
        {
            float increment = 0;
            increment += sight.Tick() ? (1 / sightWeight) * Time.deltaTime : 0;

            foreach (var sound in hearing.detectedSounds)
            {
                increment += sound * hearingWeight;
            }

            IncrementDetection(increment);
            yield return new WaitForSeconds(detectionRate);
        }
    }

    public void IncrementDetection (float amount)
    {
        float increment = amount * (1 / detectionRate);
        lastIncrement = increment;
        CurrentDetection += increment;
        if (CurrentDetection > 0)
        {
            CurrentDetection -= detectionDecay * Time.deltaTime * detectionRate;
        }
        if (detectedPlayer && CurrentDetection < 0.1f)
        {
            LosePlayer();
        } 
        else if (!detectedPlayer && CurrentDetection > detectionThreshold)
        {
            DetectPlayer();
        }
    }

    public void LosePlayer ()
    {
        // Reset
        // Path to origin
        detectedPlayer = false;
    }

    public void DetectPlayer ()
    {
        // Inform game controller
        // Aggressive
        detectedPlayer = true;
    }
    #endregion

    #region Actions
    public void StartAction (BaseAction _action, float tickRate)
    {
        if (currentAction != null && currentAction.state != BaseAction.State.Complete)
        {
            currentAction.Cancel();
        }
        currentAction = _action;
        currentAction.Begin(this, ref actionRoutine, tickRate);
    }
    #endregion
}
