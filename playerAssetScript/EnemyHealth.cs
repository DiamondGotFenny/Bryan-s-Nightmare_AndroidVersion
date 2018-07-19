using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour,IDamageable {
    public int StartingHealth = 100;
    public int currentHealth;
    public int scoreValue;
     AudioSource enemyaudio;
    public AudioClip deathClip;
    public AudioClip hurtClip;
    NavMeshAgent nav;

    Animator animt;
    LootScript loot;
    ParticleSystem hitParticle;

   public bool isDead=false;
  public  bool isDamage=false;

   
    int difficultymode;

   public string Eenemyname;
    [SerializeField] Text enemyName;
    [SerializeField] Slider health;

    [SerializeField] int StartingHealth_hardmode;
    [SerializeField] int StartingHealth_easymode;

    void Awake()
    {
        animt = GetComponent<Animator>();
        loot = GetComponent<LootScript>();
        
        hitParticle = GetComponentInChildren<ParticleSystem>();
        enemyaudio = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();     
    }



    private void Start()
    {
       difficultymode = PlayerPrefs.GetInt(GameEvent.difficultyNUM,0);
        // currentHealth = StartingHealth;
        if (difficultymode == 100)
        {
            currentHealth = StartingHealth_easymode;
            health.maxValue = StartingHealth_easymode;
        }

        if (difficultymode == 200)
        {
            currentHealth = StartingHealth;
            health.maxValue = StartingHealth;
        }

        if (difficultymode == 300)
        {
            currentHealth = StartingHealth_hardmode;
            health.maxValue = StartingHealth_hardmode;
        }
        enemyName.text = Eenemyname;
       // health.maxValue = StartingHealth;
    }

    void Update()
    {
        health.value = currentHealth;
    }
    public void TakeDamage(int amount,RaycastHit hit)
    {
        if (isDead)
            {
                return;
            }
            isDamage = true;
           nav.enabled=false;
        enemyaudio.clip = hurtClip;
        enemyaudio.Play();
        currentHealth -= amount;
        hitParticle.transform.position = hit.point;
        hitParticle.Play();
            if (currentHealth > 0)
            {
                animt.SetBool("IsDamage", true);            
            }
            if (currentHealth <= 0)
            {
                Dead();
                GameObject.Destroy(this.gameObject, 4f);
            loot.calculateLoot();
        }
    }

    public void Dead()
    {
        isDead = true;
        animt.SetBool("IsAttack", false);
        animt.SetBool("IsDamage", false);
        animt.SetTrigger("IsDead");
        GetComponent<NavMeshAgent>().enabled = false;
        if (GetComponent<SphereCollider>() !=null)
        {
            GetComponent<SphereCollider>().enabled = false;
        }    
        GetComponent<CapsuleCollider>().enabled = false;
        enemyaudio.clip = deathClip;
        enemyaudio.Play();
        Messenger<int>.Broadcast(GameEvent.ADD_SCORE, scoreValue);
    }
}
