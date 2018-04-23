using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	public float damage;
    public float range = 100f;
	int shootableMask;
	public AudioClip shootSound;

	public Camera fpsCam;
	public float fireRadius = 50f;

    private ParticleSystem gunParticleSystem;
	private AudioSource source;

	private float volLowRange = .5f;
	private float volHighRange = 1.0f;


    public Rect windowRect = new Rect(0, 0, 300, 120);



    // Use this for initialization
    void Start () {
		shootableMask = LayerMask.GetMask ("Shootable");
        gunParticleSystem = GameObject.Find("M4A1 Sopmod").GetComponent<ParticleSystem>();
        gunParticleSystem.Stop();
		damage = 1f;
		source = GetComponent<AudioSource> ();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
            Shoot();
        }
	}

    void Shoot()
    {
        Debug.Log("EnterShoot");
        RaycastHit hit;
        gunParticleSystem.Play();
		source.PlayOneShot (shootSound, 1f);

        if (Physics.Raycast(fpsCam.transform.position , fpsCam.transform.forward, out hit,fireRadius , shootableMask))
        {
            Debug.Log("Shoot:" + hit.transform.name);

            WalkerHealthScript target = hit.transform.GetComponent<WalkerHealthScript>();
			BossHealth boss = hit.transform.GetComponent<BossHealth>();
			if (target != null) {
				Debug.Log ("Damage:" + damage);
				target.TakeDamage (damage);
			} else if (boss!= null){
				Debug.Log ("Damage:" + damage);
				boss.TakeDamage (damage);
			}
        }
    }
		
}
