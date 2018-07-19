using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack2 : MonoBehaviour {

    private int _playerAttackStateHash = Animator.StringToHash("Base Layer.Attack2");
    Animator _animator;
    public int swordDamage = 30;
    RaycastHit hit;
   GameObject enemy;
    EnemyHealth enemyHealth;
    bool enemyinRange = false;

    void Awake () {
        _animator = GetComponentInParent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (other.CompareTag("Enemy"))
        {
            enemyinRange = true;
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            if (info.nameHash == _playerAttackStateHash && enemyinRange == true)
            {
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(swordDamage, hit);
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyinRange = false;
        }
    }
}
