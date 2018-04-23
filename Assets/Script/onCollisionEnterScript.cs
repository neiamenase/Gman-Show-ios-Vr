using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onCollisionEnterScript : MonoBehaviour {

	Text dialog;
	bool onPlane;

	public int status;
	public string status0Text;
	public string status1Text;
	public string status2Text;


	public bool enableStatus1;
	public bool enableStatus2;

	void Start(){
		dialog = GameObject.FindGameObjectWithTag ("Dialogue").GetComponentsInChildren<Text>()[0];
		status = 0;
		onPlane = false;
		enableStatus1 = false;

	}

	void Update(){
		if (onPlane && status == 0) {
			if (Input.GetButtonDown("Submit"))
			{
				status = 1;
			}
		}
	}



	private void OnCollisionEnter(Collision collision) {
		print (collision.gameObject.name);
		if (collision.gameObject.tag == "Player"  ) {
			onPlane = true;
			switch (status) {
			case 0:
				dialog.text = status0Text;
				break;
			case 1:
				if (enableStatus1) {
					dialog.text = status1Text;
				}
				break;
			case 2:
				if (enableStatus2) {
					dialog.text = status2Text;
				}
				break;
			}
		}
	}

	private void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Player") {
			dialog.text = "";
			onPlane = false;
		}
	}
}
