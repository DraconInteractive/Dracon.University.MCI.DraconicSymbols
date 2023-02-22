using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSkeleton : MonoBehaviour
{
    public Transform[] bones = new Transform[0];

    void Start ()
    {
        bones = transform.GetComponentsInChildren<Transform>();
    }

    void OnDrawGizmos ()
    {
        foreach (var bone in bones)
        {
            Gizmos.DrawSphere(bone.position, 0.05f);
        }
    }
}
