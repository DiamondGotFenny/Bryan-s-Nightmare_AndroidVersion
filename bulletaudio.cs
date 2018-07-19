using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletaudio : MonoBehaviour {

    AudioSource bulletSound;
	// Use this for initialization
	void Start () {
        bulletSound = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bulletSound.Play();
            Destroy(this.gameObject,1f);
        }
    }
}
