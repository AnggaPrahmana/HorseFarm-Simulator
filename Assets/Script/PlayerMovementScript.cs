using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

    public float moveSpeed;
    private Vector3 moveDirection;
	public Vector3 eulerAngleVelocity;
	public bool go = true;
	Rigidbody rb;
	Text textLog;
	string a , s;
	string [] temp;
	void Start(){
		rb = GetComponent<Rigidbody> ();
		textLog = GameObject.Find ("Log").GetComponent<Text> ();
	}

    void Update()
    {
		if(go){
			rb.MovePosition (GetComponent<Rigidbody>().position + transform.forward * 10f * Time.deltaTime);
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		}
		if (!go) {
			//GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody>().position + transform.TransformDirection(transform.forward) * 10f * Time.deltaTime); //idk this make object move to x:0;y:0;
		}

		if (Input.anyKeyDown) {
			a += "_X";
			Debug.Log (a);

		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("new log");
			a = a + "\n";
			s = a;
			textLog.text = s;
		}
    }

    void FixedUpdate()
    {
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
        //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }
}
