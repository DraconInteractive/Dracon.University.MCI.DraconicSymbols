using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    public class BootstrapGateway : MonoBehaviour
    {
        Bootstrap boot;

        private void Start()
        {
            boot = Bootstrap.Instance;
        }

        public void SetScenario(string scenario)
        {
            boot.SetScenario(scenario);
        }

        public void SetScenario(ScenarioConfig config)
        {
            boot.SetScenario(config);
        }
    }
}

