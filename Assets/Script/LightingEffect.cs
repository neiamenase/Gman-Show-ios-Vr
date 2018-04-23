using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightingEffect : MonoBehaviour {

	GameObject[] spotlights;
	public float timer;
	public float lightOnTime;
	public float lightOffTime;
	public int restartGenerator = 0;
	private bool onPlane = false;
	Text dialogue;

	EnemyManager e;


	// Use this for initialization
	void Start () {
		spotlights = GameObject.FindGameObjectsWithTag("Light");
		string n = spotlights[0].name;
		timer = 0f;
		lightOnTime = 0.3f;
		lightOffTime = 1.5f;

		lightEffect (false);
		dialogue = GameObject.FindGameObjectWithTag ("Dialogue").GetComponentsInChildren<Text> () [0];

		e = GameObject.Find ("EnemyManager").GetComponent<EnemyManager> ();
		lightEffectSetRadius ();

	}
	
	// Update is called once per frame
	void Update () {

		if (onPlane) {
			if (Input.GetButtonDown("Submit"))
			{
				restartGenerator = 1;
				dialogue.text = "";

			}
		}


		if (restartGenerator == 0) {
			if (timer > lightOffTime && timer < lightOffTime + lightOnTime) {
				lightEffect (true);
			} else if (timer > lightOffTime + lightOnTime) {
				lightEffect (false);
				timer = 0;
				lightOnTime = Random.value / 2f;
				lightOffTime = Random.value;
			}

			timer += Time.deltaTime;
		} else if (restartGenerator == 1){
			lightEffect (true);
			setDirectionalLight ();
			GameObject.Find ("electronicDoor").SetActive (false);
			restartGenerator = 2;
			e.start = true;
		}

	}

	void lightEffect(bool turnOn){
		for (int i = 0; i < spotlights.Length; i++) {
			Light l = spotlights [i].GetComponent<Light> ();
			l.enabled = turnOn;
		}
	
	}

	void lightEffectSetRadius(){
		for (int i = 0; i < spotlights.Length; i++) {
			Light l = spotlights [i].GetComponent<Light> ();
			//l.enabled = turnOn;
			l.range = 30f;
			l.intensity = 6f;
		}

	}

	void setDirectionalLight(){
		for (int i = 0; i < spotlights.Length; i++) {
			Light l = spotlights [i].GetComponent<Light> ();
			l.type = LightType.Directional;
			l.intensity = 0.3f;
		}

	}


	// Update is called once per frame

	private void OnCollisionEnter(Collision collision) {
		print (collision.gameObject.name);
		if (collision.gameObject.tag == "Player" ) {
			onPlane = true;
			dialogue.text = "Press O to restart power generator.";

		}
	}
	private void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Player") {
			onPlane = false;
			dialogue.text = "";
		}
	}
}
