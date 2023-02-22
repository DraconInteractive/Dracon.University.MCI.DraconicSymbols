using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLines : MonoBehaviour
{
    public bool renderOnUpdate;

    [System.Serializable]
    public class Arm
    {
        public Transform shoulder, elbow, hand;
        Vector3[] ArmPositions => new Vector3[3] { shoulder.position, elbow.position, hand.position };
        public LineRenderer line;

        public void Update ()
        {
            line.SetPositions(ArmPositions);
        }
    }

    public Arm left, right;

    // Update is called once per frame
    void Update()
    {
        if (renderOnUpdate)
        {
            left.Update();
            right.Update();
        }
    }
}
