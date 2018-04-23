using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerMovement : MonoBehaviour {

	GameObject player;
	Transform playerTransform;
	UnityEngine.AI.NavMeshAgent nav;
	Animator animator;
	public float fieldOfView= 100f;
	public float randomRange = 10f;
	public float idleWalkRange = 10f;
	public bool seen;
	public bool walkWhenIdle = true;
	public float attackCompleteTime = 0.5f; // zombie attack animation length = 1.13; Thus, half of it => attack successful

	PlayerHealth playerHealth;

	private SphereCollider col;
	private Vector3 nextPosition;
	private float timer = 0f; // used for 1: random attack; 2: wether the player takes damage after attack (he can doge or run)
	private float timeRange = 3f;
	private float timeBetweenAttack = 0f;



	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		animator = GetComponent<Animator>();
		col = GetComponent<SphereCollider> ();
		playerHealth = player.GetComponent<PlayerHealth>();
	}


	// Use this for initialization
	void Start () {
		
	}


	void attackEnd(){
		playerHealth.TakeDamge ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerHealth.isDead) {
			if (!seen) {
				Vector3 direction = playerTransform.transform.position - transform.position;
				float angle = Vector3.Angle (direction, transform.forward);

				// can see player?
				if (angle < fieldOfView * 0.5f) {
					RaycastHit hit;

					Vector3 gunDirectionVector = new Vector3 (transform.position.x, playerTransform.transform.position.y, transform.position.z);

					//Debug.DrawRay (gunDirectionVector, direction.normalized * 1000f, Color.blue, Time.deltaTime, true);
					if (Physics.Raycast (gunDirectionVector, direction.normalized, out hit, col.radius)) {
						if (hit.collider.gameObject.tag == "Player") {
							animator.SetBool ("isSeen", true);
							nav.SetDestination (playerTransform.transform.position);
							seen = true;
						}
					}
				} 
				if (!seen && walkWhenIdle) {
					if (timer >= timeRange) {
						nextPosition = new Vector3 (transform.position.x + Random.Range (-1 * idleWalkRange, idleWalkRange), 
							transform.position.y,
							transform.position.z + Random.Range (-1 * idleWalkRange, idleWalkRange)
						);			
						nav.SetDestination (nextPosition);
						animator.SetBool ("isRandomWalk", true);
						timer = 0f;
					} else {
						if (Vector3.Distance (nextPosition, transform.position) < 0.0001) {
							animator.SetBool ("isRandomWalk", false);
						} else {
							animator.SetBool ("isRandomWalk", true);
						}
						nav.SetDestination (nextPosition);
					}
					timer += Time.deltaTime;
				}
			} else {
				
					nav.SetDestination (playerTransform.transform.position);
					float dist = Vector3.Distance (playerTransform.position, transform.position);
					if (dist <= nav.stoppingDistance + 1.2f) {
						animator.SetBool ("isNear", true);
						timer += Time.deltaTime;
						if (timer >= attackCompleteTime + timeBetweenAttack) {
							attackEnd ();
							timer = 0f;
							timeBetweenAttack = 0.5f;
						}

					} else {
						animator.SetBool ("isNear", false);
						timer = 0f;
						timeBetweenAttack = 0f;
					}
			}
		} else {
			nav.SetDestination (transform.position);
			animator.SetBool ("isNear", false);
			animator.SetBool ("isSeen", false);
			animator.SetBool ("isRandomWalk", false);
			timer = 0f;
			timeBetweenAttack = 0f;
		}
	}
}
