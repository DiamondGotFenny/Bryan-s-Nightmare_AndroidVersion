using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGlasses : MonoBehaviour {

    public float speed=5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
   
        transform.Rotate(10*speed*Time.deltaTime, 30 * speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            Messenger.Broadcast(GameEvent.ADD_HOURGLASS);
            Destroy(gameObject);
        }
    }
}
