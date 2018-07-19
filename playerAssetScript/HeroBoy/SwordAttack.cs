using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {
    public int swordDamage = 30;
    RaycastHit hit;
    GameObject enemy;
    EnemyHealth enemyHealth;
    bool enemyinRange=false;

    void Awake () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyinRange = false;
        }
    }

    // Update is called once per frame
    void Update () {
        //if (enemyinRange && enemyHealth.currentHealth > 0)
        //{
        //    attack();
        //}

    }

    void attack()
    {
        if (enemyinRange == true && enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(swordDamage, hit);
        }
        else
        {
            return;
        }
    }

}
