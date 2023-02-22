using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BowTarget : MonoBehaviour
{
    public static List<BowTarget> All = new List<BowTarget>();
    public static BowTarget ClosestTo(Vector3 point, List<BowTarget> previous = null)
    {
        if (previous == null)
        {
            previous = new List<BowTarget>();
        }

        float dist = Mathf.Infinity;
        BowTarget output = null;
        foreach (var target in All)
        {
            if (previous.Contains(target))
            {
                continue;
            }
            float d = Vector3.Distance(target.transform.position, point);
            if (d < dist)
            {
                dist = d;
                output = target;
            }
        }
        return output;
    }

    public float Dot(Vector3 arrowPos, Vector3 arrowForward) => Vector3.Dot((transform.position - arrowPos).normalized, arrowForward);

    public float destroyTTL;

    public bool usePhysics;
    public float hitForce;

    public UnityEvent onHit;
    public UnityEvent onDestroy;

    private void OnEnable()
    {
        All.Add(this);
    }

    private void OnDisable()
    {
        All.Remove(this);
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }

    public virtual void Hit(Vector3 point, Vector3 impulse)
    {
        //Debug.Log("Target '" + name + "' was hit.");
        onHit?.Invoke();

        if (usePhysics)
        {
            if (TryGetComponent(out Rigidbody rb))
            {
                rb.AddForceAtPosition(impulse * hitForce, point);
            }
        }

        if (destroyTTL != 0)
        {
            Destroy(this.gameObject, destroyTTL);
        }
    }
}
