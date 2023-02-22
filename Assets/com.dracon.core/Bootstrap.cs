using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Works with the environment manager to load and unload packages of content
namespace Dracon.Core
{
    public class Bootstrap : MonoBehaviour
    {
        public static Bootstrap Instance;

        public ScenarioConfig startingScenario;

        [Space]
        public Transform scenarioContainer;
        public GameObject activeScenarioObject;
        public ScenarioConfig activeScenarioData;

        [Space]
        public ScenarioConfig[] allScenarios;

        private void Awake()
        {
            Instance = this;
        }

        private IEnumerator Start()
        {
            yield return null;

            if (startingScenario != null)
            {
                SetScenario(startingScenario);
            }
            else
            {
                Debug.LogError("No starting scenario!");
                Debug.Break();
            }

            yield break;
        }

        public void SetScenario(string scenario)
        {
            SetScenario(allScenarios.Where(x => x.ID == scenario).FirstOrDefault());
        }

        public void SetScenario(ScenarioConfig config)
        {
            if (activeScenarioObject != null)
            {
                Destroy(activeScenarioObject);
            }

            activeScenarioData = config;

            activeScenarioObject = Instantiate(config.scenarioObject, scenarioContainer);
            activeScenarioObject.transform.localPosition = Vector3.zero;
            activeScenarioObject.transform.localRotation = Quaternion.identity;

            if (config.environment != EnvironmentManager.Instance.activeEnvironment)
            {
                EnvironmentManager.Instance.LoadEnvironment(config.environment);
            }
        }
    }
}

