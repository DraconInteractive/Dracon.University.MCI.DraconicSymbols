using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core.Player
{
    public class PlayerHead : MonoBehaviour
    {
        public static PlayerHead Instance;

        public static Transform Transform => Instance.transform;

        private void Awake()
        {
            Instance = this;
        }
    }
}

