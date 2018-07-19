using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextLevel : MonoBehaviour {

    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        HourglassUI hourglass = GameObject.FindObjectOfType<HourglassUI>();

        if (other.tag=="Player")
        {
            Messenger.Broadcast(GameEvent.LevelChange);
            LoadScene(sceneName);
            hourglass.currentHGNumber = 0;
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
