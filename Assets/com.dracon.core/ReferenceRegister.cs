using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    public class ReferenceRegister : MonoBehaviour
    {
        public bool registerOnStart;

        public string key;
        private void Start()
        {
            if (registerOnStart)
            {
                Register();
            }
        }

        public void Register()
        {
            ReferenceManager.Instance.RegisterObject(key, this);
        }

        public void Remove()
        {
            ReferenceManager.Instance.RemoveObject(key);
        }
    }
}

