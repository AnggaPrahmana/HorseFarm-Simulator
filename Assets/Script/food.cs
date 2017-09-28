using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour {
	GameObject player;

	public GameObject parachute;
	Animator chutte;
	public float paracuteDrag;
	public float deploymentHeight;
	public bool deployed;
	Transform Planet;
	private Transform t;
	float gravity = -12;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		Planet = GameObject.FindGameObjectWithTag ("Planet").transform;


		chutte = parachute.GetComponent<Animator>();
		t = transform;
	}

	void Update(){
		player.GetComponent<playerSC>().target = transform.position;
		Ray landingRay = new Ray (transform.position, Vector3.down);
		RaycastHit hit;

		if (!deployed) {
			if (Physics.Raycast (landingRay, out hit, deploymentHeight)) {
				if (hit.collider.tag == "Planet") {
					deployParachute ();
				}
			}
		}
		if (Vector3.Distance (player.transform.position ,transform.position)<= 2f ) {
			Destroy (gameObject);
		}
	}

	void deployParachute(){

		deployed = true;
		chutte.SetInteger("ChutteTransition",1);
		GetComponent<Rigidbody> ().drag = paracuteDrag;
	}

	void OnCollisionEnter(){
		chutte.SetInteger("ChutteTransition",3);
	}

	void FixedUpdate()
	{
		Vector3 gravityUp = (t.position - Planet.position).normalized;
		Vector3 localUp = t.up;

		t.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

		Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * t.rotation;
		t.rotation = Quaternion.Slerp(t.rotation, targetRotation, 50f * Time.deltaTime);
	}
}
