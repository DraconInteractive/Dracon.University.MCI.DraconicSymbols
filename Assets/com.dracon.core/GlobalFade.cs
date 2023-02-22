using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dracon.Core
{
    public class GlobalFade : MonoBehaviour
    {
        public static GlobalFade Instance { get; private set; }

        public CanvasGroup group;
        public float speed;

        float current;
        float target;

        public static float Duration => 1 / Instance.speed;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            current = group.alpha;
        }

        // Update is called once per frame
        void Update()
        {
            if (current != target)
            {
                current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
                group.alpha = current;
            }
        }

        public void SetTarget(float _target)
        {
            target = _target;
        }

        public static void FadeIn ()
        {
            Instance.SetTarget(0);
        }

        public static void FadeOut ()
        {
            Instance.SetTarget(1);
        }
    }
}

