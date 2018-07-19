using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SwordAttack3 : MonoBehaviour {

    public int swordDamage = 30;
    public float attactRange = 2f;
    public float skillCD;
    public float skillRadius;
    public int skillDamage = 50;
    float skillTimer;

    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject skillInstantiatePoint;
    [SerializeField] GameObject skillPrefab;
    [SerializeField] Slider skillCDindicator;
    private GameObject skill;

    AudioSource swordSound;
    public AudioClip skillSound;

    Ray attackRay;
    RaycastHit attackHit;


    private void Awake()
    {
        swordSound = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        skillTimer = skillCD;
        
        skillCDindicator.maxValue = skillCD;
    }
	
	// Update is called once per frame
	void Update () {
        skillTimer += Time.deltaTime;
        skillCDindicator.value = skillTimer;
    }

    void swordAttack()
    {
        Vector3 originattack = attackPoint.transform.position;
        if (Physics.SphereCast(originattack, 0.5f,attackPoint.transform.forward, out attackHit,attactRange ))
        {
            EnemyHealth enemyHealth = attackHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(swordDamage, attackHit);
            }
        }
    }

    void skillEffect()
    {      
        if (skillTimer>skillCD)
        {
            SkillInstantiate();
            appleDamage(skillInstantiatePoint.transform.position, skillRadius, skillDamage);
        }
    }

    void SkillInstantiate()
    {
        skillTimer = 0;
        skill = Instantiate(skillPrefab) as GameObject;
        swordSound.clip = skillSound;
        swordSound.Play();
        skill.transform.position = new Vector3(skillInstantiatePoint.transform.position.x, skillInstantiatePoint.transform.position.y, skillInstantiatePoint.transform.position.z);
        skill.transform.rotation = skillInstantiatePoint.transform.rotation;
        Destroy(skill, 1f);
    }

    void appleDamage(Vector3 location, float radius, int damage)
    {
        Collider[] objsInRange = Physics.OverlapSphere(location, radius);
        
        foreach (Collider col in objsInRange)
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth !=null)
            {
                enemyHealth.TakeDamage(damage,attackHit );
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    // //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    // Gizmos.DrawWireSphere(skillInstantiatePoint.transform.position, skillRadius);
    //}
}
