using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSC : MonoBehaviour {
	Rigidbody rb;
	Animator anim;
	public float mspeed,distance;

	public Vector3 target;
	bool lookForBox;
	public bool dropableFood,hasLootBox,isBuy;
	Ray  mousePoint;
	RaycastHit hit;
	GameObject Planet;
	GameObject Stable;
	public GameObject food;
	void Start(){
		rb = GetComponent<Rigidbody> ();
		Planet = GameObject.FindGameObjectWithTag ("Planet");
		Stable = GameObject.Find ("StablePoint");
		anim = GetComponent<Animator>();
		dropableFood = true;
		hasLootBox = false;
		anim.SetInteger("PlayerTransition",0);
	}

	void Update () {
		foodCall ();

	}

	public void foodCall (){
		Vector3 mousePos = Input.mousePosition;
		mousePoint = Camera.main.ScreenPointToRay (mousePos);
		//mousePos = new Vector3 (mousePos.x, mousePos.y, food.transform.position.y);
		mousePos = new Vector3 (mousePos.x, mousePos.y, mousePos.z);
		Vector3 spawn = Camera.main.ScreenToWorldPoint (mousePos);
		if (Physics.Raycast (mousePoint, out hit)) {
			if (dropableFood) {
				if (isBuy) {
					mspeed = 2;
					isBuy = false;
					GameObject dropedFood = (GameObject)Instantiate(food);
					dropedFood.transform.position = spawn;
					dropedFood.transform.parent = Planet.transform;
					//box will give the box's position
					dropableFood = false;
					lookForBox = true;
				}
			}
		}
		distance = Vector3.Distance (target, transform.position);
		if (lookForBox) {
			anim.SetInteger("PlayerTransition",1);
			Vector3 direction = (target - transform.position).normalized;	
			Quaternion lookRot = Quaternion.LookRotation (direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, 1f * Time.deltaTime);
			if (distance==distance) {
				rb.MovePosition (rb.position + transform.forward * mspeed * Time.deltaTime);
				
			}
			if (distance <= 1.5f) {
				anim.SetInteger("PlayerTransition",0);
				hasLootBox = true;
			}
		}
		if (hasLootBox) {
			target = Stable.transform.position;
			mspeed = 3.5f;
			anim.SetInteger("PlayerTransition",2);
			if (Vector3.Distance (Stable.transform.position, transform.position) <= 7f) {
				dropableFood = true;
				hasLootBox = false;
				mspeed = 0;
				anim.SetInteger("PlayerTransition",0);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawRay (transform.position, transform.forward*5f);	
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (transform.position, target);
	}
}
