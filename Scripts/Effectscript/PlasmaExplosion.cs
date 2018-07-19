using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaExplosion : MonoBehaviour
{
    EnemyHealth enemyHealth;
    public int damage;
    float damageCD = 1f;
    float timer = 1;
    RaycastHit hit;
    // Use this for initialization
    void Start()
    {

    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {

            enemyHealth = other.GetComponent<EnemyHealth>();
            if (timer >= damageCD)
            {
                enemyHealth.TakeDamage(damage,hit);
                timer = 0;
            }
        }
    }
}