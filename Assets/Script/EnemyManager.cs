using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemy;                // The enemy prefab to be spawned.

    public float spawnTime = 0.05f;            // How long between each spawn.
    public GameObject[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int maxEnemy = 10;
	public float spawnWalkerHealth = 5f;
	public bool start;
	float timer;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
		//spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
		start = false;
    }


	void Update(){
        //if (start && timer >= spawnTime) {

        //if (start)	Spawn ();
        //Spawn();
        //	timer = 0;
        //} else {
        //	timer += Time.deltaTime;
        //}
    }

    // Update is called once per frame
    void Spawn()
    {
		if (!start)
			return;
		
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		int currentEnemy = GameObject.FindGameObjectsWithTag("Enermy").Length;
        if (currentEnemy > maxEnemy)
            return;

        enemy.GetComponent<walker>().seen = true;
		enemy.GetComponent<WalkerHealthScript> ().initialHealth = spawnWalkerHealth;
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate(enemy, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
        currentEnemy++;


    }
}