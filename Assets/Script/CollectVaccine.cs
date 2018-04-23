using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectVaccine : MonoBehaviour {
	public float timeRemaining = 30f;


	bool onPlane = false;
	float waitSec;
	int loadingTime;
	Text dialogue;
	int status;
	int temp;
	EnemyManager e;



	// Use this for initialization
	void Start () {
		dialogue = GameObject.FindGameObjectWithTag ("Dialogue").GetComponentInChildren<Text> ();
		status = 0;
		waitSec = 0f;
		temp = 0;
		e = GameObject.Find ("EnemyManager").GetComponent<EnemyManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (onPlane) {
			if (status ==0) {
				dialogue.text = "Vaccine and Virus Data Found.\nInitalizing download process.";
				status = 1;
			} else {
				if (waitSec >= 3f) {
					status = 2;
					dialogue.text = "Downloading... \nPlease stay near the console.\nTIME: " + System.Math.Round (timeRemaining, 2);
					timeRemaining = timeRemaining - Time.deltaTime;
					e.start = true;
					if (timeRemaining < 0f) {
						jumpScense ();
					}
				} else {
					waitSec = waitSec + Time.deltaTime;
					if ((waitSec > 0.5 && temp == 0) ||
						(waitSec > 1 && temp == 1) ||
						(waitSec > 1.5 && temp == 2) ||
						(waitSec > 2 && temp == 3) ||
						(waitSec > 2.5 && temp == 4)
					) {
						dialogue.text += ".";
						temp++;
					}
				}

			}

		} else {
			if (status ==2 && timeRemaining > 0f) {
				dialogue.text = "Transmitting Process Paused.\nTIME: " + System.Math.Round (timeRemaining, 2);
				e.start = false;
			}
		}
	}


	void jumpScense() {
		ChangeScene cs = new ChangeScene ();
		cs.loadNextScene ();
	}



	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			onPlane = true;
		}
	}

	private void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			onPlane = false;
		}

	}
}
