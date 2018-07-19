using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThirdPersonCamera : MonoBehaviour {

    public Transform target;
    public float smooth = 5f;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
	
	
	void FixedUpdate () {
        Vector3 destinedPosition = target.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, destinedPosition, Time.deltaTime * smooth);
        transform.position = position;
        transform.LookAt(target.transform.position);
	}
}
