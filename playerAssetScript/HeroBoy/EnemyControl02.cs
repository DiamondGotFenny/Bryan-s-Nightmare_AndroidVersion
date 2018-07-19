using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl02 : MonoBehaviour
{

    Transform playerTran;
     Animator anim;
    GameObject Player;
    PlayerHealth playerHealth;
    public int damage = 10;
    EnemyHealth enemyHealth;
    public NavMeshAgent nav;

    AudioSource enemyaudio;
    public AudioClip attackClip;

    private void Awake()
    {
        enemyaudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        anim =GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTran = Player.GetComponent<Transform>();
        playerHealth = playerTran.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            if (playerHealth != null)
            {
                nav.enabled = false;
                enemyaudio.clip = attackClip;
                enemyaudio.Play();
                anim.SetBool("IsAttack", true);
                anim.SetBool("IsWalk", false);
            }
            if (playerHealth == null)
            {
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            anim.SetBool("IsAttack", false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if ( enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0&&enemyHealth.isDamage==false)
        {
            nav.enabled = true;
            if (playerTran!=null)
            {
                nav.SetDestination(playerTran.position);
            }
            else
            {
                return;
            }
            if (nav.enabled)
            {
                anim.SetBool("IsWalk", true);
                anim.SetBool("IsIdle", false);
            }
            else
            {
                anim.SetBool("IsWalk", false);
            }
        }

        else if (playerHealth.currentHealth <= 0 || enemyHealth.isDamage == true)
        {
            nav.enabled = false;
            anim.SetBool("IsWalk", false);
        }
        else
        {
            nav.enabled = false;
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsIdle", true);
        }
    }

    void Attack()
    {
        if (playerTran!=null)
        {
            playerHealth.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void Exitdamage()
    {
        enemyHealth.isDamage = false;
    }
}
