using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    public Transform target;
    public float smooth=6f;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 tartgetposit = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, tartgetposit, smooth * Time.deltaTime);
	}
}
