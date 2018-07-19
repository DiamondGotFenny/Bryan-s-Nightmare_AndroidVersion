using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float[] spawnTime;
    public float[] startSpawnTime;

    [SerializeField] Transform[] spwanPoints;
     LootScript lootsystem;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.Current_HOURGLASS, monsterSpawnControl);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.Current_HOURGLASS, monsterSpawnControl);
    }

    void Start () {
        InvokeRepeating("SpawnEnemy0", startSpawnTime[0], spawnTime[0]);
        InvokeRepeating("SpawnEnemy1", startSpawnTime[1], spawnTime[1]);
        InvokeRepeating("SpawnEnemy2", startSpawnTime[2], spawnTime[2]);

        spawnTime[0] =3;
        spawnTime[1] = 6;
        spawnTime[2] = 24;
    }


    void SpawnEnemy0()
    {
        if (playerHealth.currentHealth<=0)
        {
            return;
        }
        if (playerHealth.currentHealth > 0)
        {
            Instantiate(enemy[0], spwanPoints[0].position, spwanPoints[0].rotation);
        }
          
    }

    void SpawnEnemy1()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }
        if (playerHealth.currentHealth > 0)
        {
            Instantiate(enemy[1], spwanPoints[1].position, spwanPoints[1].rotation);
        }
    
    }

    void SpawnEnemy2()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }
        if (playerHealth.currentHealth > 0)
        {
            Instantiate(enemy[2], spwanPoints[2].position, spwanPoints[2].rotation);
        }
      
    }

    public void monsterSpawnControl(int Currenthourglassnum)
    {
      
        if (Currenthourglassnum == 4)
        {
            lootsystem = enemy[2].GetComponent<LootScript>();
            spawnTime[0] = 9;
            spawnTime[1] = 18;
            spawnTime[2] = 42;

            lootsystem.enabled = false;
        }
    }
}
