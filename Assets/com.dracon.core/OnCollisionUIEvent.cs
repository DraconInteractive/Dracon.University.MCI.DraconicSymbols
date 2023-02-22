using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dracon.Core
{
    public class OnCollisionUIEvent : MonoBehaviour
    {
        public CollisionEvent collisionEnter, collisionExit;
        private void OnCollisionEnter(Collision collision)
        {
            collisionEnter?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            collisionExit?.Invoke(collision);
        }
    }
    // [TODO] Redo this with a primitive so it works...
    [System.Serializable]
    public class CollisionEvent : UnityEvent<Collision> { }
}

