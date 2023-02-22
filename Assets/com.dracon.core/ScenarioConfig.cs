using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    [CreateAssetMenu(menuName = "Dracon/Scenario Config")]
    public class ScenarioConfig : ScriptableObject
    {
        public string ID;
        public string displayName;
        public GameObject scenarioObject;
        public string environment;
        public Sprite icon;
        public List<ScenarioDataModule> modules = new List<ScenarioDataModule>();
    }
}

