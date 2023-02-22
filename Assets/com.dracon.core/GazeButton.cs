using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dracon.Core
{
    public class GazeButton : MonoBehaviour
    {
        public static List<GazeButton> All = new List<GazeButton>();

        public Transform lookPoint;
        [Range(0.0f, 1.0f)] // Minimum dot product value to increment selection process
        public float minDot = 0.95f;
        public float lookDuration = 1.0f; //Speed of selection

        public UnityEvent onClick;
        [HideInInspector]
        public float current;
        public GazeEvent onGazeUpdate;

        private void OnEnable()
        {
            All.Add(this);
        }

        private void OnDisable()
        {
            All.Remove(this);
        }
    }

    [Serializable]
    public class GazeEvent : UnityEvent<float>
    { }
}

