using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    public class ReferenceManager : MonoBehaviour
    {
        public static ReferenceManager Instance { get; private set; }

        [Header("Player")]
        public Transform playerHead;
        public Transform leftHand, rightHand;

#if WIT
    [Header("Voice")]
    public Oculus.Voice.AppVoiceExperience voiceExperience;
#endif

        [HideInInspector]
        public Dictionary<string, object> refMap = new Dictionary<string, object>();

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterObject(string key, object value)
        {
            if (refMap.ContainsKey(key))
            {
                refMap[key] = value;
            }
            else
            {
                refMap.Add(key, value);
            }
        }

        public void RemoveObject(string key)
        {
            if (refMap.ContainsKey(key))
            {
                refMap.Remove(key);
            }
        }
    }
}

