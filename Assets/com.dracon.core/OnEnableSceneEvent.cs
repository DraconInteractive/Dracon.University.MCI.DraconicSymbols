using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dracon.Core
{
    public class OnEnableSceneEvent : MonoBehaviour
    {
        public UnityEvent onEnable;
        private void OnEnable()
        {
            onEnable?.Invoke();
        }
    }
}

