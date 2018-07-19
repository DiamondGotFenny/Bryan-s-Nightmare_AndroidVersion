using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLifescript : MonoBehaviour {
    public GameObject player;
   PlayerHealth playerHealth;
   public int healthAdd = 50;

    private void Awake()
    {
       playerHealth = player.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
          // playerHealth.AddLife(healthAdd);
            Messenger<int>.Broadcast(GameEvent.ADD_LIFE, healthAdd);
            Destroy(this.gameObject);        
        }
    }
}
