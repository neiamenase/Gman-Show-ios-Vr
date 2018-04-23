using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBoss : MonoBehaviour {

	Boss bossScript;
	GameObject boss;
	NextScene next;
	Text dialog;
	public int status;
	bool onPlane;
	//ChangeScene next;


	void Start(){
		boss = GameObject.Find("Boss");
		bossScript = boss.GetComponent<Boss> ();
		dialog = GameObject.FindGameObjectWithTag ("Dialogue").GetComponentsInChildren<Text>()[0];
		status = 0;
		onPlane = false;
		boss.SetActive (false);

		next = GameObject.Find("NextScene").GetComponent<NextScene>();
		next.enable = false;
	}

	void Update(){
		if (status < 2) {
			//	next.allowJump = false;
		}
		if (onPlane && status == 0) {
			if (Input.GetButtonDown("Submit"))
			{
				boss.SetActive (true);
				bossScript.start = true;
				status = 1;
				dialog.text = "Contamination found in room. Auto-lock applied";
			}
		}
	}



	private void OnCollisionEnter(Collision collision) {
		print (collision.gameObject.name);
		if (collision.gameObject.tag == "Player"  ) {
			onPlane = true;
			switch (status) {
			case 0:
				dialog.text = "Press O to unseal ALL locks.";
				break;
			case 1:
				dialog.text = "Contamination found in room. Auto-lock applied";
				break;
			case 2:
				dialog.text = "Door unlocked";
				next.enable = true;
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
