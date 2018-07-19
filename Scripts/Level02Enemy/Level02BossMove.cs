using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level02BossMove : MonoBehaviour {

    Transform playerTran;
    Animator anim;
    GameObject Player;
    PlayerHealth playerHealth;
    public int damage = 20;
    public float skillCD = 10f;
    bool isLive = true;

    EnemyHealth enemyHealth;
   public NavMeshAgent nav;
    float skillTimer;

    AudioSource enemyaudio;
    public AudioClip attackClip;
    public AudioClip showClip;
    public AudioClip skillSound;

    [SerializeField] GameObject skillPrebab;
    [SerializeField] GameObject SkillProjectile;

    private GameObject skill;

    private void Awake()
    {
        enemyaudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTran = Player.GetComponent<Transform>();
        playerHealth = playerTran.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        enemyaudio.clip = showClip;
        enemyaudio.Play();
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
        animControl();
    }

    void Attack()
    {
        if (playerTran != null)
        {
            playerHealth.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void animControl()
    {
        skillTimer += Time.deltaTime;
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {

            nav.enabled = true;
            if (playerTran != null)
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

            if (Vector3.Distance(playerTran.position, this.transform.position) < 10
                && Vector3.Distance(playerTran.position, this.transform.position) >2
                && skillTimer > skillCD&&skill==null)
            {
                skillTimer = 0;
                nav.enabled = false;
                anim.SetBool("IsSkill", true);
                enemyaudio.clip = skillSound;
                enemyaudio.PlayDelayed(0.2f);
            }
            else
            {
                nav.enabled = true;
                anim.SetBool("IsSkill", false);
            }
        }

        else
        {
            nav.enabled = false;
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsIdle", true);
        }
    }

    void skillInstantiate()
    {
        skill = Instantiate(skillPrebab) as GameObject;
        skill.transform.position = new Vector3(SkillProjectile.transform.position.x, SkillProjectile.transform.position.y, SkillProjectile.transform.position.z);
        skill.transform.rotation = this.transform.rotation;
        Destroy(skill, 2f);
    }
}
