using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    public class FacePlayer : MonoBehaviour
    {
        Transform playerHead;
        public bool isUI;
        private void Start()
        {
            playerHead = ReferenceManager.Instance.playerHead;
        }

        private void Update()
        {
            Vector3 dir = playerHead.position - transform.position;
            dir.Normalize();

            transform.rotation = Quaternion.LookRotation(isUI ? -dir : dir, Vector3.up);
        }
    }
}

