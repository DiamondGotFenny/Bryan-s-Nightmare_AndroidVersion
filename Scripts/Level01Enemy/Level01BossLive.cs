using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01BossLive : MonoBehaviour {
    EnemyHealth Bosshealth;
	// Use this for initialization
	void Start () {
        Bosshealth =gameObject.GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Bosshealth.currentHealth<=0)
        {
            GameObject[] enemyclones = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemyclone in enemyclones)
            {
                Destroy(enemyclone);
            }
        }
	}
}
