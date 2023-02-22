using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsway : MonoBehaviour 
{
	public Transform root;
	Vector3 rotationLast;
	Vector3 rotationDelta;
	public float rotationAmount = 4f;


	void Start () 
	{
		rotationLast = root.transform.eulerAngles;

	}


	void LateUpdate () 
	{
		rotationDelta = root.transform.eulerAngles - rotationLast;

		if (Mathf.Abs (rotationDelta.y) < .5f)
		{
			rotationDelta.y = 0;
		}
		if (Mathf.Abs (rotationDelta.x) < 0.5f)
		{
			rotationDelta.x = 0f;
		}

		rotationDelta.y = Mathf.Clamp(rotationDelta.y,-1f,1f);
		rotationDelta.x =  Mathf.Clamp(rotationDelta.x,-1f,1f);

		rotationLast = root.transform.eulerAngles;
		//transform.localEulerAngles = rotationDelta;

		Vector3 wantedrotation = new Vector3 (rotationDelta.x, rotationDelta.y, -rotationDelta.y);
		wantedrotation *= rotationAmount;
		transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(wantedrotation),Time.deltaTime * .5f);
	}
	public Vector3 angularvelocity
	{
		get
		{
			return rotationDelta;


		}
	}
}
