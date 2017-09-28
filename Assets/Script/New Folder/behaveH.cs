using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaveH : MonoBehaviour {

	public List<listHorse> otherHorse;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(otherHorse.Count > 0){
			for(int i = 0; i < otherHorse.Count;i++){
				otherHorse[i].myDistance = Vector3.Distance(transform.position, otherHorse[i].pos);
				if(otherHorse[i].myDistance > 14){
					otherHorse.Remove(otherHorse[i]);
				}
			}
		}
	}

	public void foundHorse(listHorse _Horse){
		otherHorse.Add(_Horse);
	}
}
