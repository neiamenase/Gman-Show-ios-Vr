using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour {

	GameObject player;
	PlayerHealth playerHealth;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision){
		if (collision.collider.tag == "Player" && playerHealth.currentHealth <10) {
			playerHealth.Recover ();
			gameObject.SetActive (false);
		}
	}
}
