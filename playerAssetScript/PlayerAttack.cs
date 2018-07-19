using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    Animator anim;
    public int damage;
    private List<Transform> enemiesinrange = new List<Transform>();
    RaycastHit shootHit;

    void Awake () {
        anim = GetComponent<Animator>();
	}
	


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetEnemiesInRange();
            damage = 10;
            anim.SetBool("IsKick",true);
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetEnemiesInRange();
            damage = 5;
            anim.SetBool("IsPunch1",true);
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetEnemiesInRange();
            damage = 10;
            anim.SetBool("IsPunch2",true);
            Attack();
        }
    }

    void GetEnemiesInRange()
    {
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * 0.5f), 0.5f))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                enemiesinrange.Add(c.transform);
            }
        }
        
    }

    void Attack()
    {              
        foreach (Transform enemy in enemiesinrange)
        {
            enemiesinrange.Clear();
            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
            if (eh == null)
            {
                continue;
            }
            eh.TakeDamage(damage,shootHit);
        }
    }
}
