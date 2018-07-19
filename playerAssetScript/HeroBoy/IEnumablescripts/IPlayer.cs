using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : LivingEntity {

    public float movingspeed = 6f;
    Camera viewCamera;
   
	// Use this for initialization
	protected override void Start () {
        base.Start();
        viewCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
