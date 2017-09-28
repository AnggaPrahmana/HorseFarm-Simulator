using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class listHorse {
	public int maxSpeed, horsePower;
	public Vector3 pos;
	public float myDistance;
	
	public listHorse(Vector3 _pos){
		pos = _pos;

		horsePower = Random.Range(400,600);

		if(horsePower>500){
			maxSpeed = Random.Range(7,10);
		}
		else if(horsePower<500){
			maxSpeed = Random.Range(4,6);
		}
	}
	
}
