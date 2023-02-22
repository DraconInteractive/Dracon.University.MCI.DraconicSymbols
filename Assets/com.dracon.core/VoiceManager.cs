using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using System.Linq;

#if WIT

using Facebook.WitAi.Data;
using Facebook.WitAi.Lib;
using Facebook.WitAi;
using Facebook.WitAi.Data.Traits;

namespace Dracon.Core.Voice
{
    public class VoiceManager : MonoBehaviour
    {
        private Dictionary<string, VoiceIntent> intentMap = new Dictionary<string, VoiceIntent>()
        {
            {"testing", VoiceIntent.Testing },
            {"greeting", VoiceIntent.Greeting },
            {"wit$cancel", VoiceIntent.Cancel },
            {"wit$confirmation", VoiceIntent.Confirm },
            {"wit$go_back", VoiceIntent.GoBack },
            {"wit$go_forward", VoiceIntent.GoForward },
            {"wit$negation", VoiceIntent.Negate },
            {"wit$nevermind", VoiceIntent.Nevermind },
            {"wit$repeat_response", VoiceIntent.Repeat },
            {"", VoiceIntent.None }
        };
        private Dictionary<string, VoiceTrait> traitMap = new Dictionary<string, VoiceTrait>()
        {
            {"wit$sentiment", VoiceTrait.Sentiment },
            {"", VoiceTrait.None }
        };

        private static VoiceManager instance;
        public static VoiceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    return FindObjectOfType<VoiceManager>();
                } else
                {
                    return instance;
                }
            }   
        }

        [Range(0.0f, 1.0f)]
        public float intentConfidenceThreshold = 0.9f;
        [Range(0.0f, 1.0f)]
        public float traitConfidenceThreshold = 0.9f;

        public UnityEvent<VoiceResponse> onResponse;

        public Queue<VoiceResponse> received = new Queue<VoiceResponse>();

        private void Awake()
        {
            instance = this;
            onResponse.AddListener((x) =>
            {
                Debug.Log($"{x.intent} -> Traits: {x.traits.Count}\n{x.transcript}");
            });
        }

        public void ResponseReceived (WitResponseNode response)
        {
            var firstIntent = response.GetFirstIntent();
            var intentConfidence = firstIntent["confidence"].AsFloat;

            var traits = GetTraits(response);

            //string output = FormDebugOutput(response, firstIntent, intentConfidence, traits);
            //Debug.Log(output);

            var vResponse = new VoiceResponse(response);

            if (intentConfidence > intentConfidenceThreshold)
            {
                try
                {
                    vResponse.intent = intentMap[firstIntent["name"].Value];
                    vResponse.intentNode = firstIntent;
                }
                catch
                {
                    Debug.LogError($"Missing intentMap entry for [{firstIntent["name"].Value}]");
                }
            }
        
            foreach (var trait in traits)
            {
                if (trait.Item3 > traitConfidenceThreshold)
                {
                    vResponse.traits.Add((traitMap[trait.Item1], trait.Item2, trait.Item3));
                }
            }

            received.Enqueue(vResponse);
            onResponse?.Invoke(vResponse);
        }

        public string FormDebugOutput (WitResponseNode response, WitResponseNode firstIntent, float confidence, List<(string, WitResponseNode, float)> traits)
        {
            string output = "";
            output += $"Intent: {firstIntent["name"].Value}\n";
            output += $"\tConfidence: {confidence}\n\n";


            output += "Traits:\n";
            for (int i = 0; i < traits.Count; i++)
            {
                output += $"{traits[i].Item1}: \n";
                output += $"\tConfidence: {traits[i].Item3}\n";
                output += $"\tValue: {traits[i].Item2["value"].Value}\n";
            }

            output += $"\n{response.ToString()}";
            return output;
        }
    
        public List<(string, WitResponseNode, float)> GetTraits (WitResponseNode response)
        {
            var traitCount = response["traits"].Count;

            var N = SimpleJSON.JSON.Parse(response["traits"].ToString());
            List<(string, WitResponseNode, float)> traits = new List<(string, WitResponseNode, float)>();

            List<string> keys = new List<string>();
            foreach (string n in N.Keys)
            {
                keys.Add(n);
            }

            for (int i = 0; i < traitCount; i++)
            {
                var node = response["traits"][i][0];
                traits.Add((keys[i], node, node["confidence"].AsFloat));
            }

            return traits;
        }
    }

    public class VoiceResponse
    {
        public string transcript;
        public WitResponseNode coreNode;
        public VoiceIntent intent;
        public WitResponseNode intentNode;
        public List<(VoiceTrait, WitResponseNode, float)> traits;

        public VoiceResponse(WitResponseNode core)
        {
            coreNode = core;
            transcript = core["text"].Value;
            intent = VoiceIntent.None;
            traits = new List<(VoiceTrait, WitResponseNode, float)>();
        }
    }

    public enum VoiceIntent
    {
        None,
        Confirm,
        Negate,
        Testing,
        Cancel,
        GoBack, 
        GoForward,
        Nevermind,
        Repeat,
        Greeting
    }

    public enum VoiceTrait
    {
        None,
        Sentiment
    }
}
#endif

