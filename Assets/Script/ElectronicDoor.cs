using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectronicDoor : MonoBehaviour {

	Text dialogue;
	// Use this for initialization
	void Start () {
		dialogue = GameObject.FindGameObjectWithTag ("Dialogue").GetComponentsInChildren<Text> () [0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision) {
		print (collision.gameObject.name);
		if (collision.gameObject.tag == "Player" ) {
			dialogue.text = "It is an electronic door. \nPlease restart power generator.";

		}
	}
}
