using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIK : MonoBehaviour
{
    [System.Serializable]
    public class HandMap
    {
        public Transform wrist;
        public Transform index1;
        public Transform index2;
        public Transform index3;
        public Transform mid1;
        public Transform mid2;
        public Transform mid3;
        public Transform ring1;
        public Transform ring2;
        public Transform ring3;
        public Transform pinky1;
        public Transform pinky2;
        public Transform pinky3;
        public Transform thumb1;
        public Transform thumb2;
        public Transform thumb3;
    }
    [System.Serializable]
    public class HandOffset
    {
        public Vector3 wrist;
        public Vector3 i1;
        public Vector3 i2;
        public Vector3 i3;
        public Vector3 m1;
        public Vector3 m2;
        public Vector3 m3;
        public Vector3 r1;
        public Vector3 r2;
        public Vector3 r3;
        public Vector3 p1;
        public Vector3 p2;
        public Vector3 p3;
        public Vector3 t1;
        public Vector3 t2;
        public Vector3 t3;
    }

    public HandMap origin;
    public HandMap target;
    public HandOffset offset;

    public Space offsetSpace;
    public bool useWrist;
    private void LateUpdate()
    {
        if (useWrist)
        {
            target.wrist.rotation = origin.wrist.rotation;
            target.wrist.Rotate(offset.wrist, offsetSpace);
        }
        target.index1.rotation = origin.index1.rotation;
        target.index1.Rotate(offset.i1, offsetSpace);
        target.index2.rotation = origin.index2.rotation;
        target.index2.Rotate(offset.i2, offsetSpace);
        target.index3.rotation = origin.index3.rotation;
        target.index3.Rotate(offset.i3, offsetSpace);
        target.mid1.rotation = origin.mid1.rotation;
        target.mid1.Rotate(offset.m1, offsetSpace);
        target.mid2.rotation = origin.mid2.rotation;
        target.mid2.Rotate(offset.m2, offsetSpace);
        target.mid3.rotation = origin.mid3.rotation;
        target.mid3.Rotate(offset.m3, offsetSpace);
        target.ring1.rotation = origin.ring1.rotation;
        target.ring1.Rotate(offset.r1, offsetSpace);
        target.ring2.rotation = origin.ring2.rotation;
        target.ring2.Rotate(offset.r2, offsetSpace);
        target.ring3.rotation = origin.ring3.rotation;
        target.ring3.Rotate(offset.r3, offsetSpace);
        target.pinky1.rotation = origin.pinky1.rotation;
        target.pinky1.Rotate(offset.p1, offsetSpace);
        target.pinky2.rotation = origin.pinky2.rotation;
        target.pinky2.Rotate(offset.p2, offsetSpace);
        target.pinky3.rotation = origin.pinky3.rotation;
        target.pinky3.Rotate(offset.p3, offsetSpace);
        target.thumb1.rotation = origin.thumb1.rotation;
        target.thumb1.Rotate(offset.t1, offsetSpace);
        target.thumb2.rotation = origin.thumb2.rotation;
        target.thumb2.Rotate(offset.t2, offsetSpace);
        target.thumb3.rotation = origin.thumb3.rotation;
        target.thumb3.Rotate(offset.t3, offsetSpace);

    }
}
