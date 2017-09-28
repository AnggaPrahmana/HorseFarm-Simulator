using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLight : MonoBehaviour {

	// Use this for initialization
	Light light;
	void Start () {
		light = GetComponent<Light>();
		StartCoroutine(Spotlight());
	}
	
	IEnumerator Spotlight(){
		while(true){
			yield return new WaitForSeconds(0.05f);
			light.intensity = Random.RandomRange(5,10);
		}
	}
}
