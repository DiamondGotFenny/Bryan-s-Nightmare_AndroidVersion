using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EenemyManager : MonoBehaviour {

    public  PlayerHealth playerHealth;
    public GameObject enemy;
    //public EnemyControl02 enemy;
    public float spawnTime = 8f;
    public float waitToStart = 8f;
    //public Transform[] spawnPoint;
    public Transform spawnPoint;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", waitToStart, spawnTime);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
        if (playerHealth.currentHealth<=0)
        {
            return;
        }

        // int spawnPointIndex = Random.Range(0, spawnPoint.Length);

        //GameObject enemyclone=   Instantiate(enemy, spawnPoint[spawnPointIndex].position, spawnPoint[spawnPointIndex].rotation);
        GameObject enemyclone = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
       //EnemyControl02 enemyclone= Instantiate(enemy, spawnPoint.position, spawnPoint.rotation) as EnemyControl02;
    }
}
