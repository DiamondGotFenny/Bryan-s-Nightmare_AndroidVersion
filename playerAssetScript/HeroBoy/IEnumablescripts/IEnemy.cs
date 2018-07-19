using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(NavMeshAgent))]
public class IEnemy : LivingEntity {
    // NavMeshAgent pathFinder;
    Transform target;
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
