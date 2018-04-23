using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySight : MonoBehaviour {
	public float fieldOfView= 100f;
	public bool playerInSight;

	private UnityEngine.AI.NavMeshAgent nav;
	private SphereCollider col;
	private Animator anim;
	private GameObject player;

	void Awake(){
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
		anim = GetComponent<Animator> ();

		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
			playerInSight = false;

			Vector3 direction = player.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			if (angle < fieldOfView * 0.5f) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, col.radius)) {
					if (hit.collider.gameObject == player) {
						playerInSight = true;
						anim.SetBool ("isSeen", true);
						nav.SetDestination (player.transform.position);
					}
				}
			}
	}


	void OnTriggerStay(Collider other){
//		if (other.gameObject == player) {
//			playerInSight = false;
//
//			Vector3 direction = other.transform.position - transform.position;
//			float angle = Vector3.Angle (direction, transform.forward);
//
//			if (angle < fieldOfView * 0.5f) {
//				RaycastHit hit;
//				if (Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, col.radius)) {
//					if (hit.collider.gameObject == player) {
//						playerInSight = true;
//						anim.SetBool ("isSeen", true);
//						nav.SetDestination (player.transform.position);
//					}
//				}
//			}
//		}
	}

}

