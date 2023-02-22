using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    //A platform for forwarding function calls from scriptable objects etc into the scene
    public class ActionObject : MonoBehaviour
    {
        public static ActionObject Instance { get; private set; }

        public delegate void OnAction(string action);
        public OnAction onAction;

        private void Awake()
        {
            Instance = this;
            onAction += DebugAction;
        }

        public void RunAction(string _action)
        {
            onAction(_action);
        }

        public void DebugAction(string _action)
        {
            Debug.Log("Action: " + _action);
        }

        public void DebugAyncAction(Action _action)
        {
            Debug.Log("AsyncAction: " + _action.value);
        }
    }

    [System.Serializable]
    public class Action
    {
        public enum Type
        {
            None,
            Registered,
            LoadScenario,
            WaitForEnvironment
        }

        public Type type;

        public string value;

        public enum RunTrigger
        {
            OnEnter,
            OnExit
        }
        public RunTrigger runTrigger;

        public enum Status
        {
            NotStarted,
            Running,
            Complete
        }

        public Status status;

        public void Run()
        {
            switch (type)
            {
                case Type.None:
                    status = Status.Complete;
                    break;
                case Type.Registered:
                    ActionObject.Instance.RunAction(value);
                    break;
                case Type.WaitForEnvironment:
                    ActionObject.Instance.StartCoroutine(AsyncRun());
                    break;
                case Type.LoadScenario:
                    status = Status.Running;
                    ActionObject.Instance.StartCoroutine(AsyncRun());
                    break;
            }
        }

        IEnumerator AsyncRun()
        {
            switch (type)
            {
                case Type.LoadScenario:
                    status = Status.Running;
                    Bootstrap.Instance.SetScenario(value);
                    while (EnvironmentManager.Loading)
                    {
                        yield return null;
                    }
                    status = Status.Complete;
                    break;
                case Type.WaitForEnvironment:
                    status = Status.Running;
                    while (EnvironmentManager.Loading)
                    {
                        yield return null;
                    }
                    status = Status.Complete;
                    break;
            }
        }
    }
}

