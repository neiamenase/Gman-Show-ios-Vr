using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    GameObject player;
    Vector3 cmareaoffset;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        cmareaoffset = new Vector3(0, 1, -2);

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + cmareaoffset;
	}
}
