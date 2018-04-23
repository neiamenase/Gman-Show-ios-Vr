using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerHealthScript : MonoBehaviour {

    public float initialHealth = 3f;
	public float currentHealth = 0;
	Animator animator;
	public float dieCompleteTime = 15f;
	private float timer;
	public bool isDead = false;
    private ParticleSystem WalkerParticleSystem;
	walker w;

    void Start () {
		currentHealth = initialHealth;
		animator = GetComponent<Animator>();
		timer = 0;

        WalkerParticleSystem = GetComponent<ParticleSystem>();
        WalkerParticleSystem.Stop();

		w = GetComponent<walker> ();
    }

	void Update(){
		if (isDead) {
			Die ();
		}
	}


    public void TakeDamage(float amount)
    {
        WalkerParticleSystem.Play();
        Debug.Log("Current HP:" + amount);
		currentHealth -= amount;
		w.seen = true;
		if(currentHealth <= 0f)
        {
			isDead = true;
        }
    }

    public void Die()
    {
		if (timer == 0) {
			animator.SetBool ("attackFront", true);
		}
		if (timer >= dieCompleteTime) {
			Destroy (gameObject);
			timer = 0;
			isDead = false;
		}
		timer += Time.deltaTime;
    }
}
