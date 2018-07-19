using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03Monster_EnemyAttack : MonoBehaviour {
    public float timeBetweenAttact = 0.5f;
    public int attactDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerhealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerhealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")                                                         //if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenAttact && playerInRange && enemyHealth.currentHealth >0)
        {
            Attack();
        }    
    }

    void Attack()
    {
        timer = 0;
        if (playerhealth.currentHealth > 0)
        {
            playerhealth.TakeDamage(attactDamage);
        }
    }
}
