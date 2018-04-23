using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementLimit : MonoBehaviour {

	// Use this for initialization
	GameObject player;
	Transform playerT;
	private float xmin = -49.3f;
	private float xmax = 16.7f;
	private float zmin = -64f;
	private float zmax = -10f;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerT = player.transform;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = playerT.position;
		pos.x = Mathf.Clamp(pos.x, xmin, xmax);
		pos.z = Mathf.Clamp(pos.z, zmin , zmax);
		playerT.position = pos;
	}
}
