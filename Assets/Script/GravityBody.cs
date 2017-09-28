using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {

	public PlanetScript attractorPlanet;
	private Transform t;

	void Start()
	{
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

		t = transform;
	}

	void FixedUpdate()
	{
		if (attractorPlanet)
		{
			attractorPlanet.Attract(t);
		}
	}
}
