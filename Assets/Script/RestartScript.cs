using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour {

	public bool isWin = false;


	// Use this for initialization
	void Start () {

		Text text = this.GetComponentInChildren<Text>();
		ChangeScene cs = new ChangeScene();
		if (cs.getSceneName(cs.getSceneID()) == "Explosion") {
			isWin = true;
			text.text = "but you have managed to compelete the mission...";
		} else {
			isWin = false;
			text.text = "Continue?    Yes(O)  No(X)";
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Return"))
		{
			restart ();
		}
		else if (Input.GetButtonDown("Submit"))
		{
			if (!isWin)
				retry ();
			else
				restart ();
		}
	}


	void restart(){
		ChangeScene cs = new ChangeScene();
		cs.loadStartScene ();
	}

	void retry(){
		ChangeScene cs = new ChangeScene();
		cs.loadRetryScene ();

	}
}
