using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	GameObject player;
	private Rigidbody rb;
	Transform playerTransform;
	public UnityEngine.AI.NavMeshAgent nav;
	Animator animator;
	public float montionCompleteTime = 0.5f; // zombie attack animation length = 1.13; Thus, half of it => attack successful
	public bool start = false;
	public bool inMotion = false;

	private float stopTimer;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController s;
	public bool beingAttack;

	public float overrideStopTime = 0f;
	public bool isStart = false;

	PlayerHealth playerHealth;
	BossHealth bossHealth;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		animator = GetComponent<Animator>();
		playerHealth = player.GetComponent<PlayerHealth>();
		rb = player.GetComponent<Rigidbody>();
		s = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ();
		s.movementSettings.ForwardSpeed = 12f;
		s.movementSettings.BackwardSpeed = 8f;
		stopTimer = -1f;
		inMotion = false;
		bossHealth = GetComponent<BossHealth> ();
	}

	// Update is called once per frame
	void Update () {
		if (start) {
			animator.SetTrigger ("startTrigger");
			start = false;
			isStart = true;
		}

		if (!playerHealth.isDead && isStart && !bossHealth.isDead) {
			animator.SetBool ("start", false);
			if (inMotion) {
				nav.SetDestination (transform.position);
				if (animator.GetBool ("isPlayerNear")) {
					animator.SetBool ("isPlayerNear", false);
					animator.SetInteger ("stopType", Random.Range (1, 3));
					overrideStopTime = 0.5f;
					inMotion = true;
				} else {
					float stopTime = montionCompleteTime;
					if (overrideStopTime > 0)
						stopTime = overrideStopTime;
					if (stopTimer >= stopTime) {
						inMotion = false;
						overrideStopTime = 0f;
					}
					stopTimer += Time.deltaTime;
				}
			} else {
				stopTimer = 0f;
				float dist = Vector3.Distance (playerTransform.position, transform.position);
				bool isNear = (dist <= nav.stoppingDistance + 0.5f);

				if (isNear && beingAttack) { // isnear and being attacked
					nav.SetDestination (transform.position);
					attackEnd ();
					animator.SetInteger ("stopType", Random.Range (1, 3));
					overrideStopTime = 1.5f;
					inMotion = true;
					beingAttack = false;

				} else if (isNear) {
					nav.SetDestination (transform.position);
					animator.SetBool ("isPlayerNear", true);
					int type = Random.Range (1, 4);
					animator.SetInteger ("attackType", type);
					inMotion = true;
					if (type == 1 || type ==3) {
						overrideStopTime = 0.01f;
					} else if (type == 2) {
						overrideStopTime = 0.05f;
					}
					beingAttack = true;
				} else {
					if (beingAttack) {
						beingAttack = false;
						animator.SetInteger ("stopType", 0);
					}
					if (animator.GetBool ("isPlayerNear")) {
						animator.SetBool ("isPlayerNear", false);
						animator.SetInteger ("stopType", Random.Range (1, 3));
						overrideStopTime = 1.5f;
						inMotion = true;
					} else {
						nav.SetDestination (playerTransform.transform.position);
					}
				}
			}
		}
	}

	void attackEnd(){
		playerHealth.TakeDamge ();
		if (playerHealth.isDead) {
			animator.SetBool ("isPlayerDead", true);
		} else {
			rb.velocity = (player.transform.position + transform.forward * 200f);
		}
	}
}