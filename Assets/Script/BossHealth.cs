using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

	public float initialHealth = 100f;
	public float currentHealth = 0;
	Animator animator;
	public float dieCompleteTime = 15f;
	private float timer;
	public bool isDead = false;
	Boss bossScript;
	private ParticleSystem particleSystem;
	private bool isInDanger;
	OpenBoss openScript;

	void Start () {
		currentHealth = initialHealth;
		animator = GetComponent<Animator>();
		timer = 0;
		bossScript = GetComponent<Boss> ();
		particleSystem = GetComponent<ParticleSystem>();
		particleSystem.Stop();
		isInDanger = false;
		openScript = GameObject.Find ("Computer").GetComponent<OpenBoss>();

	}

	void Update(){
		if (isDead) {
			Die ();
		}
	}


	public void TakeDamage(float amount)
	{
		particleSystem.Play();
		if (bossScript.isStart) {
			Debug.Log ("Current HP:" + amount);
			currentHealth -= amount;

			if (currentHealth < 100 && currentHealth % 10 == 0 && currentHealth > 50) {
				animator.SetTrigger ("getHit");
				bossScript.inMotion = true;
				bossScript.overrideStopTime = 1.7f;
			}

			if (!isInDanger && currentHealth < 50) {
				animator.SetBool ("isInDanger", true);
				bossScript.nav.speed = 15f;
				isInDanger = true;
			}


			if (currentHealth <= 0f && !isDead) {
				animator.SetTrigger ("getHit");
				bossScript.inMotion = true;
				bossScript.overrideStopTime = 1.7f;
				animator.SetBool ("isDead", true);
				isDead = true;
				openScript.status = 2;
			}
		}
	}

	public void Die()
	{
		if (timer == 0) {
			animator.SetBool ("gethit", true);

		}
		if (timer >= dieCompleteTime) {
			Destroy (gameObject);
			timer = 0;
			isDead = false;
		}
		timer += Time.deltaTime;
	}
}
