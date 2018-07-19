using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

     Transform playerTran;
    static Animator anim;
     GameObject Player;
    PlayerHealth playerHealth;
    public int damage = 10;
    EnemyHealth enemyHealth;

    void Awake () {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTran = Player.GetComponent<Transform>();
        playerHealth =playerTran.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
	}
	
	
	void Update () {
        if (Vector3.Distance(playerTran.position,this.transform.position)<10&& enemyHealth.currentHealth > 0&&playerHealth.currentHealth>0)
        {
            Vector3 direction = playerTran.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                Quaternion.LookRotation(direction), 1f);
            anim.SetBool("IsIdle", false);
            if (direction.magnitude>1.5f)
            {
                this.transform.Translate(0, 0, 0.01f);
                anim.SetBool("IsWalk", true);
                anim.SetBool("IsAttack", false);
                //Debug.Log(direction.magnitude);
            }
            else if(playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
            {
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsAttack", true);
                this.transform.Translate(0, 0, 0);
                //if (playerHealth.currentHealth > 0)  call this event in animation
                //{
                //    Attack();
                //}             
            }

           else if (playerHealth.currentHealth <= 0|| enemyHealth.currentHealth<=0)
            {
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsAttack", false);
                anim.SetBool("IsDead", true);
                
            }

        }
        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsAttack", false);
            this.transform.Translate(0, 0, 0);
        }
	}

    void Attack()
    {
       
            playerHealth.TakeDamage(damage);       
                
    }

  
}
