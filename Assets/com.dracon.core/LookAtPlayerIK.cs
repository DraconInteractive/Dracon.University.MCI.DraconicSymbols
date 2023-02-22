using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    public class LookAtPlayerIK : MonoBehaviour
    {
        public Animator anim;

        public Transform lookTarget;
        public float lookSpeed;

        [Range(0, 1)]
        public float lookWeight;

        Vector3 lookPosition;

        private void OnValidate()
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
        }

        private void Start()
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }

            if (lookTarget == null)
            {
                lookTarget = Camera.main.transform;
            }

            anim.SetLookAtPosition(lookTarget.position);
        }

        private void Update()
        {
            lookPosition = Vector3.MoveTowards(lookPosition, lookTarget.position, lookSpeed * Time.deltaTime);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            anim.SetLookAtPosition(lookPosition);
            anim.SetLookAtWeight(lookWeight);
        }

        IEnumerator FadeInWeight()
        {
            for (float f = 0; f < lookWeight; f += Time.deltaTime)
            {
                anim.SetLookAtWeight(f);
                yield return null;
            }
        }
    }
}

