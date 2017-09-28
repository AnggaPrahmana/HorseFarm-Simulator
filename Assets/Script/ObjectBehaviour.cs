using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ObjectBehaviour : MonoBehaviour {

	public float distance;
	Transform leader;

	//MOVE BUSINESS
	public float moveSpeed;
	public bool go = true;
	Rigidbody rb;


	//LOG ACTIVITY ATRR
	public Text UI_status;
	Text textLog;
	public gameManager GM;
	string temp,s;

	void Start(){
		leader = GameObject.Find ("AlphaHorse").transform;
		rb = GetComponent<Rigidbody> ();
		textLog = GameObject.Find ("Log").GetComponent<Text> ();
	}

	void Update()
	{
		if (gameObject.name != "AlphaHorse") {
			if (go) {
				rb.MovePosition (GetComponent<Rigidbody> ().position + transform.forward * moveSpeed * Time.deltaTime);
			}

			flocking ();
		} else if (gameObject.name == "AlphaHorse") {

		}
	}

	void flocking(){
		distance = Vector3.Distance (transform.position, leader.position);
		if (distance > 5f) {
			lookAtLeader ();
			s = gameObject.name + ") looking for leader";
			if (temp != s) {
				temp = s;
				GM.AddToLog (temp);
				UI_status.text = temp;
			}
		}
		if (distance < 5f) {
			doAlignment ();
			s = gameObject.name + ") align with leader";
			if (temp != s) {
				temp = s;
				GM.AddToLog (temp);
				UI_status.text = temp;
			}
			if (distance < 2f) {
				while (moveSpeed > 0) {
					moveSpeed --;
					s = gameObject.name + ") arrive";
					if (temp != s) {
						temp = s;
						GM.AddToLog (temp);
					UI_status.text = temp;
					}
				}
			}
			if (distance > 3f) {
				while (moveSpeed < 3f) {
					moveSpeed ++;
					s = gameObject.name + ") approach";
					if (temp != s) {
						temp = s;
						GM.AddToLog (temp);
						UI_status.text = temp;
					}
				}
			}
		}
	}

	void lookAtLeader(){
		Vector3 direction = (leader.position - transform.position).normalized;	
		Quaternion lookRot = Quaternion.LookRotation (direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, 1f * Time.deltaTime);
	}

	void doAlignment(){
		transform.rotation = Quaternion.Slerp (transform.rotation, leader.rotation, 1f * Time.deltaTime);
	}

	void OnDrawGizmos(){
		Gizmos.DrawRay (transform.position, transform.forward*5f);
	}
}
