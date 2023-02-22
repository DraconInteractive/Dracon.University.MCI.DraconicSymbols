using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
    [Range(0.3f, 1)]
    public float MaxDistance = 0.3f;
    private float Yaw;
    private float Pitch;
    private Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        cam.nearClipPlane = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
        cam.transform.localPosition = Vector3.back*MaxDistance;
        MaxDistance = Mathf.Clamp(MaxDistance - Input.GetAxis("Mouse ScrollWheel"), 0.3f, 1);
        Yaw = (Yaw + Input.GetAxis("Mouse X")) % 360;
        Pitch = Mathf.Clamp(Pitch - Input.GetAxis("Mouse Y"), -90, 90);
        transform.rotation = Quaternion.Euler(Pitch, Yaw, 0);
	}
}
