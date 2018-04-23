using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExpositionScript : MonoBehaviour {


	public float timer;
	public Color textColor = Color.white;

	private Text t1;
	private Text t2;
	private float fadeFactor = 1f;

	// Use this for initialization
	void Start () {
		timer = 0f;
		GameObject canvas = GameObject.Find ("Canvas");
		//Text x = panel.GetComponent<UnityEngine.UI.Text> ();
		Text[] temp = canvas.GetComponentsInChildren<Text> ();
		if (temp [0].name == "Text1") {
			t1 = temp [0];
			t2 = temp [1];
		} else {
			t1 = temp [1];
			t2 = temp [0];
		}
		t1.color = Color.clear;
		t2.color = Color.clear;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Menu"))
		{
			ChangeScene  cs = new ChangeScene();
			cs.loadMenuScene ();
		}
		if (timer < 3f) {
			t1.color = Color.Lerp (t1.color, textColor, fadeFactor * Time.deltaTime);
		}else if(timer < 5f) {
			t1.color = Color.Lerp (t1.color, Color.clear, fadeFactor * Time.deltaTime);
		} else if (timer < 8f) {
			t2.color = Color.Lerp (t2.color, textColor, fadeFactor * Time.deltaTime);
		} else if (timer < 11f) {
			t2.color = Color.Lerp (t2.color, Color.clear, fadeFactor * Time.deltaTime);
		} else {
			t2.color = Color.clear; // for parparing restart
			t1.color = Color.clear; 
			// Jump Scense
			ChangeScene  cs = new ChangeScene();
			cs.writeSaveData (10, 0);
			cs.loadNextScene ();
		}
		timer += Time.deltaTime;
	}
}
