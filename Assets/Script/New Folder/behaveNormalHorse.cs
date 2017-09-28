using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaveNormalHorse : MonoBehaviour {
	public listHorse normalHorse;
	public List<listHorse> otherHorse;

	public enum horseState{
		flock,
		wander,
		idle,
		feed
	}

	public horseState state;
	//list horse yang disekitar normal horse
	// Use this for initialization
	void Start () {
		state = horseState.idle;
		normalHorse = new listHorse(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		normalHorse.pos = transform.position;
		
		if(state == horseState.idle){
			print("idle");
		}
		else if(state == horseState.flock){
			print("flock");
		}
	}

	public void foundHorse(listHorse _horse){
		otherHorse.Add(_horse);
	}
}
