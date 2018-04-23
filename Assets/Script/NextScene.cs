using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextScene : MonoBehaviour {

	public bool enable;
	bool onPlane;
	Text dialogue;

	// Use this for initialization
	void Start () {
		onPlane = false;
		enable = true;


		dialogue = GameObject.FindGameObjectWithTag ("Dialogue").GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onPlane && enable) {
			if (Input.GetButtonDown("Submit"))
			{
				ChangeScene cs = new ChangeScene ();
				cs.loadNextScene ();
			}
		}
	}
		

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player" ) {
			onPlane = true;
			if (enable) {
				dialogue.text = "Press O to next scene";
			} else {
				dialogue.text = "Door locked";
			}
		}
	}

	private void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Player" ) {
			onPlane = false;
			dialogue.text = "";
		}
	}
}
