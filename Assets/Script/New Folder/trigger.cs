using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {

	// Use this for initialization
	public GameObject horse;

	void OnTriggerEnter(Collider other){
		if(other.tag == "Horse"){
			if(other.name != horse.name){
				other.GetComponent<behaveNormalHorse>().state = behaveNormalHorse.horseState.flock;
			}
		}
	}
}
