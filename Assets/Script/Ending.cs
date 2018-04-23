using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {

	GameObject endingText;
	GameObject explosion; 
	public float timer;

	// Use this for initialization
	void Start () {
		//endingText = this.GetComponentsInChildren<Texture> ();
		InvokeRepeating("Spawn", 0.4f, 0.4f);
		explosion = GameObject.FindGameObjectWithTag ("Explosion");
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= 6f) {
			ChangeScene cs = new ChangeScene();
			cs.loadDeadScene ();
		}
		timer += Time.deltaTime;
	}


	void Spawn()
	{
		Instantiate( explosion , new Vector3(Random.Range(-10,50), 4f, Random.Range(-10, 50)), new  Quaternion (0,0,0,0));
	}
}
