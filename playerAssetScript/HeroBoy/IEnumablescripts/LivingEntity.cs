using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable {

    public float startingHealth = 100f;
    protected float currentHealth;
    protected bool isDead;
	// Use this for initialization
	protected virtual void Start () {
        currentHealth = startingHealth;
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage, RaycastHit hit)
    {
        currentHealth -= damage;
        if (currentHealth<=0&&!isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
        GameObject.Destroy(gameObject, 4f);
    }
}
