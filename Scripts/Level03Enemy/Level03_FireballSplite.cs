using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03_FireballSplite : MonoBehaviour {

    AudioSource explodeSoundCtl;

    public int damage = 20;
    private void Update()
    {
        Destroy(this.gameObject, 2f);
    }

    private void Start()
    {
        GetComponent<AudioSource>().Play();
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

