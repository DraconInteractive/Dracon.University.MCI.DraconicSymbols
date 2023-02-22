using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    [System.Serializable]
    public class Bezier
    {
        public Transform[] controlPoints;
        public float segmentCount;

        [HideInInspector]
        public Vector3[] points;
        private int curveCount = 0;
        public Bezier(Transform[] _controlPoints, float _segmentCount = 50)
        {
            controlPoints = _controlPoints;
            segmentCount = _segmentCount;
            Generate();
        }

        public void Generate()
        {
            if (controlPoints == null || controlPoints.Length < 4 || segmentCount < 1)
            {
                return;
            }

            curveCount = (int)controlPoints.Length / 3;
            List<Vector3> _points = new List<Vector3>();

            for (int j = 0; j < curveCount; j++)
            {
                for (int i = 0; i < segmentCount; i++)
                {
                    float t = i / segmentCount;
                    int nodeIndex = j * 3;
                    Vector3 v = CalculateCubicBezierPoint(t, controlPoints[nodeIndex].position, controlPoints[nodeIndex + 1].position, controlPoints[nodeIndex + 2].position, controlPoints[nodeIndex + 3].position);
                    _points.Add(v);
                }
            }

            points = _points.ToArray();
        }

#if UNITY_EDITOR
    public void GizmoDebug ()
    {
        if (controlPoints == null || controlPoints.Length < 4 || segmentCount < 1 || points == null) return;

        for (int i = 0; i < points.Length; i++)
        {
            if (i < points.Length - 1)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }    
        }
        foreach (var transform in controlPoints)
        {
            Gizmos.DrawWireSphere(transform.position, 0.02f);
        }
    }
#endif

        Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
        }
    }
}

