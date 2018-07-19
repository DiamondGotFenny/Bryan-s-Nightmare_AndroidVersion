using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour {

    PlayerHealth playerHealth;
    public  int damage;
    float damageCD = 1f;
    float timer = 1;

    private void Start()
    {
        float timer = 1;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
    
        if (other.tag=="Player")
        {
            
           playerHealth = other.GetComponent<PlayerHealth>();
            if (timer >= damageCD)
            {
                playerHealth.TakeDamage(damage);
                timer = 0;
            }
           
        }
    }
}
