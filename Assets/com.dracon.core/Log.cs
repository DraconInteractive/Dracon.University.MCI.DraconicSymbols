using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dracon.Core
{
    public class Log : MonoBehaviour
    {
        public static Log Instance;
        public Dictionary<int, string> output = new Dictionary<int, string>();

        public TMP_Text logText;

        private void Awake()
        {
            Instance = this;
        }

        public static void Add(int ID, string message)
        {
            if (!Instance.output.ContainsKey(ID))
            {
                Instance.output.Add(ID, message);
            }
            else
            {
                Instance.output[ID] = message;
            }

            Instance.UpdateText();
        }


        public void UpdateText()
        {
            string _out = "";
            foreach (var i in output)
            {
                _out += i.Value + "\n";
            }
            logText.text = _out;
        }
    }
}

