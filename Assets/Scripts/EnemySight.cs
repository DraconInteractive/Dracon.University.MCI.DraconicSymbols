using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [System.Serializable]
    public class SightDebug
    {
        public bool result;
        public float dist;
        public bool distInRange;
        public float dot;
        public bool dotValid;
        public bool rayValid;

        public SightDebug(bool _result = false, float _dist = 0, bool _distInRange = false, float _dot = 0, bool _dotValid = false, bool _rayValid = false)
        {
            result = _result;
            dist = _dist;
            distInRange = _distInRange;
            dot = _dot;
            dotValid = _dotValid;
            rayValid = _rayValid;
        }
    }

    public Transform eyes;
    public float range;

    [Range(0f, 1f)]
    public float dotRange;

    Transform target;
    [Space]
    public SightDebug debug;

    private void Start()
    {
        target = Dracon.Core.ReferenceManager.Instance.playerHead;    
    }

    public bool Tick ()
    {
        bool result = true;
        float dist = Vector3.Distance(eyes.position, target.position);
        if (dist >= range)
        {
            result = false;
            debug = new SightDebug(false, dist);
            return result;
        }

        Vector3 dir = target.position - eyes.position;
        float dot = Vector3.Dot(eyes.forward, dir.normalized);
        if (dot < dotRange)
        {
            result = false;
            debug = new SightDebug(false, dist, dist < range, dot, dot > range);
            return result;
        }

        Ray ray = new Ray(eyes.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            result = false;
            debug = new SightDebug(false, dist, dist < range, dot, dot > range);
            return result;
        }
        debug = new SightDebug(false, dist, dist < range, dot, dot > range, true);

        return result;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (eyes == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyes.position, range);
    }
#endif
}
