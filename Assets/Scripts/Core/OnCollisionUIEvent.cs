using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

[System.Serializable]
public class CollisionEvent : UnityEvent<Collision> { }
