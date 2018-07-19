using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level03Monster_BossMovement : MonoBehaviour {

    Transform playerTran;
    Animator anim;
    GameObject Player;
    PlayerHealth playerHealth;
    public int damage = 20;
    public float timeBetweenFireball = 3f;

    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    float fireBallTimer;

    AudioSource enemyaudio;
    public AudioClip attackClip;
    public AudioClip showClip;
    public AudioClip skillSound;

    [SerializeField] GameObject fireBallPrebab;
    [SerializeField] GameObject fireBallProjectile;

    private GameObject _fireBall;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTran = Player.GetComponent<Transform>();
        playerHealth = playerTran.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        enemyaudio = GetComponent<AudioSource>();
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
                anim.SetBool("IsAttack", true);
                anim.SetBool("IsWalk", false);
                enemyaudio.clip = attackClip;
                enemyaudio.Play();
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
        AnimationCont();
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

    void Exitdamage()
    {
        enemyHealth.isDamage = false;
    }

    void AnimationCont()
    {
        fireBallTimer += Time.deltaTime;
        if ( enemyHealth.currentHealth > 0  && enemyHealth.isDamage == false)
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

            Ray ray = new Ray(fireBallProjectile.transform.position,fireBallProjectile.transform.forward);
            if (Vector3.Distance(playerTran.position, this.transform.position) < 20&& 
                Vector3.Distance(playerTran.position, this.transform.position) > 10&& fireBallTimer > timeBetweenFireball)
            {              
                RaycastHit hit;
                if (Physics.SphereCast(ray,0.75f,out hit))
                {                   
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerHealth>())
                    {
                        if (_fireBall==null)
                        {
                            fireBallTimer = 0;
                            nav.enabled = false;
                            anim.SetBool("IsFireball", true);
                        }
                    }                  
                }                    
            }
            else
            {
                nav.enabled = true;
                anim.SetBool("IsFireball", false);
            }
        }
        else
        {
            nav.enabled = false;
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsIdle", true);
        }
    }

    void fireballInstantiate()
    {
        enemyaudio.clip = skillSound;
        enemyaudio.PlayDelayed(0.2f);
        _fireBall = Instantiate(fireBallPrebab) as GameObject;
        _fireBall.transform.position =fireBallProjectile.transform.TransformPoint(Vector3.forward );
        _fireBall.transform.rotation = fireBallProjectile.transform.rotation ;
    }
}
