using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winningscript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            Messenger<bool>.Broadcast(GameEvent.Win_GAME,true);
        }
    }
}
