using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01BossSkill : MonoBehaviour {

    public float speed = 10f;
    public float oriRadius = 1f;
    public float maxRadius = 8.5f;
    public int damage = 40;
    float dymicCollider;

    SphereCollider skillRange;

    private void Awake()
    {
        skillRange = GetComponent<SphereCollider>();
    }
    // Use this for initialization
    void Start () {
        skillRange.radius = oriRadius;
	}
	
	// Update is called once per frame
	void Update () {
       oriRadius = Mathf.MoveTowards(oriRadius, maxRadius, speed*Time.deltaTime );
        skillRange.radius = oriRadius;
        if (skillRange.radius == maxRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }      
    }
}
