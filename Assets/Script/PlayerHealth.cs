using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 10;
	public int currentHealth;
	public Slider healthSlider;
	public RawImage damageImage;
	public float flashSpeed = 0.5f;
	public Color flashColor = new Color(1f, 0f, 0f);
	public bool isDead;

	bool damaged;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerMovement;
	private List <RawImage> HPbars;

	// Use this for initialization
	void Start () {
		ChangeScene cs = new ChangeScene();
		currentHealth = cs.getHealthData();
		print (currentHealth);

		//currentHealth = startingHealth;
		playerMovement = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
		damageImage = GameObject.Find ("DamageImage").GetComponents<RawImage>()[0];
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("HP");
		HPbars = new List<RawImage> ();
		for (int i = 0; i < objs.Length; i++){
			HPbars.Add (objs [i].GetComponent<RawImage> ());
		}
		HealthChange ();
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}
	public void TakeDamge(){
		damaged = true;
		currentHealth -= 1;
		HealthChange ();
		if (currentHealth <= 0 && !isDead) {
			HealthChange ();
			Death ();
		}
	}
	public void Death(){
		isDead = true;
		playerMovement.enabled = false;
		// should show a dialog asking whether they want to restart
		ChangeScene cs = new ChangeScene();
		cs.loadDeadScene ();
	}
	public void Recover(){
		currentHealth += 1;
		HealthChange ();
	}
	public void HealthChange(){
		Color HPBarColor;
		if (currentHealth >= 8) {
			HPBarColor = new Color (0f, 1f, 0f); // green
		} else if (currentHealth >= 5) {
			HPBarColor = new Color (1f, 1f, 0f); // yellow
		} else if (currentHealth >= 3) {
			HPBarColor = new Color (1f, 0.549f, 0f); // orange
		} else{
			HPBarColor = new Color (1f, 0f, 0f); // red
		}
		for (int i = 0; i < HPbars.Count; i++) {
			if (i +1 <= currentHealth) {
				HPbars.Find (x => x.name == "HP" + (i + 1)).color = HPBarColor;
			} else {
				HPbars.Find (x => x.name == "HP" + (i + 1)).color = Color.clear;
			}
		}
	}
	public int getCurrentHealth(){
		return currentHealth;
	}
}
