using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dracon.Core
{
    public class ScenarioObject : MonoBehaviour
    {
        public GameObject propsContainer, charactersContainer, uiContainer;
        public Canvas scenarioPrimaryUI;

#if UNITY_EDITOR
        [ContextMenu("Setup")]
        public void Setup ()
        {
            Undo.RecordObject(this, "Setup");
            if (propsContainer == null)
            {
                Transform props = transform.Find("Props");
                if (props != null)
                {
                    propsContainer = props.gameObject;
                }
                else
                {
                    propsContainer = new GameObject("Props");
                    propsContainer.transform.parent = this.transform;
                }
            }

            if (charactersContainer == null)
            {
                Transform characters = transform.Find("Characters");
                if (characters != null)
                {
                    charactersContainer = characters.gameObject;
                }
                else
                {
                    charactersContainer = new GameObject("Characters");
                    charactersContainer.transform.parent = this.transform;
                }
            }

            if (uiContainer == null)
            {
                Transform UI = transform.Find("UI");
                if (UI != null)
                {
                    uiContainer = UI.gameObject;
                }
                else
                {
                    uiContainer = new GameObject("UI");
                    uiContainer.transform.parent = this.transform;
                }
            }
        }
#endif
    }
}

