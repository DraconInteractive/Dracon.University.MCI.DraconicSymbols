using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dracon.Core;

public class Arrow : MonoBehaviour
{
    public float movementSpeed;
    public float ttl = 5;
    public float dropRate;
    public float pathCorrectionBase;
    float pathCorrection;
    public Transform tip;

    protected Coroutine launchR;
    protected bool launched;
    protected Vector3 velocity;
    protected BowTarget target;

    public GameObject[] activateOnLaunch;

    public virtual void Launch(float power, BowTarget target, float correctionMult = 0)
    {
        Log.Add(-5, "Target: " + (target == null ? "NULL" : "AVAILABLE"));
        if (target != null)
        {
            Quaternion targetRot = Quaternion.LookRotation((target.transform.position - transform.position).normalized, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, correctionMult);
        }
        

        if (correctionMult != 0)
        {
            pathCorrection = pathCorrectionBase * correctionMult;
        }

        transform.SetParent(null);
        movementSpeed *= power;
        launchR = StartCoroutine(LaunchRoutine(target));
        launched = true;

        foreach (GameObject go in activateOnLaunch)
        {
            go.SetActive(true);
        }
    }

    protected virtual IEnumerator LaunchRoutine(BowTarget _target)
    {
        target = _target;
        while (ttl > 0)
        {
            ttl -= Time.deltaTime;

            Vector3 tipPos_last = transform.position;

            dropRate += dropRate * Time.deltaTime;

            Vector3 delta = transform.forward * movementSpeed + Vector3.down * dropRate;
            delta *= Time.deltaTime;

            velocity = delta;
            transform.position += delta;
            transform.rotation = Quaternion.LookRotation(delta.normalized, Vector3.up);

            if (target != null)
            {
                Vector3 targetDir = target.transform.position - transform.position;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir.normalized, Vector3.up), pathCorrection);
            }


            Ray ray = new Ray(tipPos_last, delta.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, delta.magnitude))
            {
                if (hit.transform.TryGetComponent(out BowTarget t))
                {
                    Hit(t, hit);
                }
                else
                {
                    Log.Add(-1, "[-1] Non-Target collider named '" + hit.transform.name + "' in target layer. Changing name to OBS_TARGET");
                    hit.transform.name = "OBS_TARGET";
                    BaseHit(hit);
                }
            }
            yield return null;
        }
        Destroy(this.gameObject);
        yield break;
    }

    protected virtual void Hit(BowTarget t, RaycastHit hit)
    {
        Vector3 tipDelta = tip.position - transform.position;

        if (launched)
        {
            t.Hit(tip.position, velocity);
            StopCoroutine(launchR);

            transform.position = hit.point - (tipDelta * 0.95f);
            Destroy(this.gameObject, 3);
        }
    }

    protected virtual void BaseHit(RaycastHit hit)
    {
        Vector3 tipDelta = tip.position - transform.position;

        if (launched)
        {
            StopCoroutine(launchR);
            transform.position = hit.point - (tipDelta * 0.95f);
            Destroy(this.gameObject, 3);
        }
    }
}
