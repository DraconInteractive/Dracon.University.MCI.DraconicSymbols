using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Dracon.Core.Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        InteractionsManager iManager;
        
        public List<BaseModule> modules = new List<BaseModule>();
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            iManager = InteractionsManager.Instance;
            foreach (var mod in modules) 
            {
                mod.StartEx();
            }
        }

        private void Update()
        {
            foreach (var mod in modules) 
            {
                mod.UpdateEx();
            }
        }
        
        private void OnEnable () 
        {
            foreach (var mod in modules) 
            {
                mod.OnEnableEx();
            }
        }
        
        private void OnDisable () 
        {
            foreach (var mod in modules) 
            {
                mod.OnDisableEx();
            }
        }
        
        private void OnDestroy () 
        {
            foreach (var mod in modules)
            {
                mod.OnDestroyEx();
            }
        }
    }
}

