using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move this out of dracon core eventually into its own thing. 

namespace Dracon.Core
{
    public class BaseNPC : MonoBehaviour
    {
        public string ID;

        public GameObject model, colliders;
        //TODO: Add behaviour tree (node canvas?)

        protected virtual void OnValidate()
        {
            if (GUI.changed)
            {
                SetName();
            }
        }

        protected virtual void SetName()
        {
            gameObject.name = $"NPC_{ID}";
        }

        [ContextMenu("Init Dependencies")]
        protected virtual void InitializeOfflineDependencies()
        {
            var _model = transform.Find("Model");
            if (_model == null)
            {
                GameObject go = new GameObject("Model");
                go.transform.SetParent(this.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                model = go;
            }
            else
            {
                model = _model.gameObject;
            }

            var _colliders = transform.Find("Colliders");

            if (_colliders == null)
            {
                GameObject go = new GameObject("Colliders");
                go.transform.SetParent(this.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                colliders = go;
            }
            else
            {
                colliders = _colliders.gameObject;
            }
        }

    }
}

