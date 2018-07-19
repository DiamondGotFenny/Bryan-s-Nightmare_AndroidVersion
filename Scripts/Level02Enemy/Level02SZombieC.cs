using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level02SZombieC : MonoBehaviour {

    GameObject player;
    Transform PlayerTran;
    Animator animt;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    bool attacking=false;

    AudioSource enemyaudio;
    public AudioClip attackClip;

    private void Awake()
    {
        enemyaudio = GetComponent<AudioSource>();
    }

    public int damage;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerTran =player.GetComponent<Transform>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        animt = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerHealth.currentHealth>0&&enemyHealth.currentHealth>0&&attacking==false)
        {
            nav.enabled = true;
            nav.SetDestination(PlayerTran.position);
        }
        else
        {
            nav.enabled = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==player)
        {
            if (playerHealth != null)
            {
                nav.enabled = false;
                enemyaudio.clip = attackClip;
                enemyaudio.Play();
                animt.SetBool("IsAttack", true);
                attacking = true;
            }
            if (playerHealth == null)
            {
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            nav.enabled = true;
            animt.SetBool("IsAttack", false);
            attacking = false;
        }
    }

    void Attack()
    {
        if (PlayerTran != null)
        {
            playerHealth.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
}
