using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if WIT
namespace Dracon.Core.Voice
{
    public class VoiceAction : MonoBehaviour
    {
        public string prompt;
        public UnityEvent voiceAction;

        private void OnEnable()
        {
            VoiceManager.Instance.onResponse.AddListener(CheckResponse);
        }

        private void OnDisable()
        {
            VoiceManager.Instance.onResponse.RemoveListener(CheckResponse);
        }

        private void CheckResponse(VoiceResponse response)
        {
            if (response.transcript.ToLower().Contains(prompt))
            {
                voiceAction?.Invoke();
            }
        }
    }
}
#endif

