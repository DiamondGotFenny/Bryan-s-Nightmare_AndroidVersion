using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01_textPopup : MonoBehaviour {
    [SerializeField] GameObject popuText;
    GameObject player;
    // Use this for initialization

    AudioSource entrySFX;
    public AudioClip noEnter;

    private void Awake()
    {
        popuText.SetActive(false);
    }
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        entrySFX = GetComponent<AudioSource>();
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==player)
        {
            popuText.SetActive(true);
            entrySFX.clip = noEnter;
            entrySFX.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            popuText.SetActive(false);
        }
    }
}
