using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03EnemyManager : MonoBehaviour {

    PlayerHealth playerHealth;
    [SerializeField] Transform[] spawnPoint;
    HourglassUI hourglass;
    public GameObject[] enemy;
    public float[] SpawnCD;
    public float spawnStartTime;
    public float[] timer;
    int difficultymode;

    // Use this for initialization
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        difficultymode = PlayerPrefs.GetInt(GameEvent.difficultyNUM, 0);
        if (difficultymode == 100)
        {
            OnEasyMode();
        }
        if (difficultymode == 200)
        {
            OnNormalMode();
        }
        if (difficultymode == 300)
        {
            OnHardMode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
        {
            SpawnCDControl();
        }
    }

    void spawnEnemy0()
    {
        Instantiate(enemy[0], spawnPoint[0].transform.position, spawnPoint[0].transform.rotation);
    }

    void spawnEnemy1()
    {
        Instantiate(enemy[1], spawnPoint[1].transform.position, spawnPoint[1].transform.rotation);
    }

    void spawnEnemy2()
    {
        Instantiate(enemy[2], spawnPoint[2].transform.position, spawnPoint[2].transform.rotation);
    }

    void SpawnCDControl()
    {
        timer[0] += Time.deltaTime;
        if (timer[0] >= SpawnCD[0] && playerHealth.currentHealth > 0)
        {
            Invoke("spawnEnemy0", spawnStartTime);
            timer[0] = 0;
        }

        timer[1] += Time.deltaTime;
        if (timer[1] >= SpawnCD[1] && playerHealth.currentHealth > 0)
        {
            Invoke("spawnEnemy1", spawnStartTime);
            timer[1] = 0;
        }

        timer[2] += Time.deltaTime;
        if (timer[2] >= SpawnCD[2] && playerHealth.currentHealth > 0)
        {
            Invoke("spawnEnemy2", spawnStartTime);
            timer[2] = 0;
        }

        hourglass = GameObject.FindObjectOfType<HourglassUI>();
        if (hourglass != null)
        {
            AddGlasses[] hourGlass0 = GameObject.FindObjectsOfType<AddGlasses>();
            if (hourGlass0.Length + hourglass.currentHGNumber >= 4)
            {
                var clones = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemyclone in clones)
                {
                    if (enemyclone.name == "Hellephant(Clone)")
                    {
                        enemyclone.GetComponent<LootScript>().dropChance = 0;
                    }
                }
            }
        }
    }

    void OnHardMode()
    {
        SpawnCD[0] = 4f;
        SpawnCD[1] = 10f;
        SpawnCD[2] = 14f;
    }

    void OnNormalMode()
    {
        SpawnCD[0] = 8f;
        SpawnCD[1] = 14f;
        SpawnCD[2] = 18f;
    }

    void OnEasyMode()
    {
        SpawnCD[0] = 8f;
        SpawnCD[1] = 14f;
        SpawnCD[2] = 18f;
    }
}
