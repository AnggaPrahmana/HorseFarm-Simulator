using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neighborTrigger : MonoBehaviour {

	public GameObject horse;
	public int horseType;

	void OnTriggerEnter(Collider other){
		if(horseType == 1){
			if(other.tag == "Horse"){
			if(!horse.GetComponent<behaveH>().otherHorse.Contains(other.GetComponent<behaveNormalHorse>().normalHorse)){ //CEK SELURUH KUDA YANG ADA
				horse.GetComponent<behaveH>().foundHorse(other.GetComponent<behaveNormalHorse>().normalHorse);
				Debug.Log("ketemu kudo");
				other.GetComponent<behaveNormalHorse>().state = behaveNormalHorse.horseState.flock;
			}
		}
		} else if(horseType == 2){
			if(other.tag == "Horse"){
				if(!horse.GetComponent<behaveNormalHorse>().otherHorse.Contains(other.GetComponent<behaveNormalHorse>().normalHorse)){
					horse.GetComponent<behaveNormalHorse>().foundHorse(other.GetComponent<behaveNormalHorse>().normalHorse);
					horse.GetComponent<behaveNormalHorse>().state = behaveNormalHorse.horseState.flock;
				}
			} else if(other.tag == "HorseLeader"){

			}
		}
		
	}

	void OnTriggerExit(Collider other){

	}
}
