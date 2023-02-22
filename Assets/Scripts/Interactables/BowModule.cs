using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowModule : InteractableModule
{
    public Interactable notch;
    public LineRenderer bowString;

    public Transform top, bottom, rest;
    public GameObject[] arrowPrefabs;
    public int arrowIndex;
    Arrow spawnedArrow;
    public float notchRecoverySpeed;
    public float minimumForce;

    [Range(0, 1)]
    public float aimAssist = 1;
    public GameObject aimObject;
    Coroutine grabR;

    Vector3 ArrowDir => (rest.position - notch.transform.position).normalized;
    float LaunchForce => Mathf.Clamp01(Vector3.Distance(notch.transform.position, Vector3.Lerp(top.position, bottom.position, 0.5f)));

    public override void LateUpdateEx()
    {
        base.LateUpdateEx();

        if (!notch.InHand)
        {
            Vector3 targetPos = Vector3.Lerp(top.position, bottom.position, 0.5f);
            notch.transform.position = Vector3.MoveTowards(notch.transform.position, targetPos, notchRecoverySpeed * Time.deltaTime);
        }

        //notch.transform.rotation = Quaternion.LookRotation(ArrowDir, Vector3.up);
        notch.transform.LookAt(rest);

        Vector3[] pos = new Vector3[3]
        {
            top.position,
            notch.transform.position,
            bottom.position
        };

        bowString.SetPositions(pos);
    }

    public override void OnInteract()
    {
        base.OnInteract();

        arrowIndex++;
        if (arrowIndex >= arrowPrefabs.Length)
        {
            arrowIndex = 0;
        }
    }

    public void NotchGrab()
    {
        //Create arrow if bow is held
        if (InHand)
        {
            //create arrow
            spawnedArrow = Instantiate(arrowPrefabs[arrowIndex], notch.transform.position, notch.transform.rotation, notch.transform).GetComponent<Arrow>();
            spawnedArrow.transform.localRotation = Quaternion.identity;
            grabR = StartCoroutine(GrabRoutine());
        }
    }

    public void NotchRelease()
    {
        //if arrow exists, launch it
        if (spawnedArrow != null)
        {
            Launch();
            StopCoroutine(grabR);
            aimObject.SetActive(false);
        }
    }

    IEnumerator GrabRoutine()
    {
        aimObject.SetActive(true);
        while (true)
        {
            if (spawnedArrow != null)
            {
                var bestTarget = BestTarget();
                if (bestTarget != null)
                {
                    Vector3 targetPoint = bestTarget.transform.position;
                    Quaternion targetRot = Quaternion.Lerp(spawnedArrow.transform.rotation, Quaternion.LookRotation((targetPoint - spawnedArrow.transform.position).normalized, Vector3.up), aimAssist);
                    aimObject.transform.position = spawnedArrow.tip.position;
                    aimObject.transform.rotation = targetRot;
                    aimObject.transform.position = aimObject.transform.forward * Vector3.Distance(spawnedArrow.tip.position, targetPoint);
                }
            }
            yield return null;
        }
    }

    BowTarget BestTarget()
    {
        Vector3 nPos = notch.transform.position;
        Vector3 aDir = ArrowDir;

        float bd = -1;
        BowTarget bestTarget = null;
        foreach (var t in BowTarget.All)
        {
            float d = t.Dot(nPos, aDir);

            if (d > bd && d > 0.2f)
            {
                bd = d;
                bestTarget = t;
            }
        }
        return bestTarget;
    }

    void Launch()
    {
        float force = LaunchForce;

        if (force < minimumForce)
        {
            Destroy(spawnedArrow.gameObject);
            return;
        }

        var bestTarget = BestTarget();
        spawnedArrow.GetComponent<Arrow>().Launch(force, bestTarget, aimAssist);

        //Debug.Log("Shot Result:\nBest Target: " + bestTarget.name + "\nLaunch force: " + force.ToString());

        spawnedArrow = null;

    }
}
