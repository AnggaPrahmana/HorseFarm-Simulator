using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinTrigger : MonoBehaviour {

	public Transform target, pin;

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "HorseLeader") {
			//pin.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "HorseLeader") {
			//pin.gameObject.SetActive(false);
		}
	}
}
