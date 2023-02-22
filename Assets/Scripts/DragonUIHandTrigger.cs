using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonUIHandTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            float lDist = Vector3.Distance(transform.position, Interactor.left.transform.position);
            float rDist = Vector3.Distance(transform.position, Interactor.right.transform.position);

            if (lDist > rDist)
            {

            }
            else
            {

            }
        }
    }
}
