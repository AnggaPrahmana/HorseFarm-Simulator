using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinScript : MonoBehaviour {

	public Transform target;

	float smoothSpeed = 0.125f;
	Vector3 offset;

	void LateUpdate(){
		Vector3 desiredPos = target.position + offset;
		Vector3 smoothedPos = Vector3.Lerp (transform.position, desiredPos, smoothSpeed);
		transform.position = smoothedPos;
	}
}
