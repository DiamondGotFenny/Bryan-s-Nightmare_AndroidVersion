using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour {

    public GameObject AKRifle;
    public GameObject LightSaber;
    public GameObject muzzle;

    public AudioClip lightsabersound;
    public AudioClip rifleSafety;

    AudioSource Lightsabershow;

    public bool AKrifle;
    public bool Lightsaber;

    bool equtAK;
    bool eutSword;

    private void Awake()
    {
        Lightsabershow= GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        AKrifle = true;
        Lightsaber = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (equtAK==true && AKrifle ==false)
        {
            Lightsabershow.clip = rifleSafety;
            Lightsabershow.Play();
            AKrifle = true;
            Lightsaber = false;
        }

        if (eutSword==true&& Lightsaber == false)
        {
            Lightsabershow.clip = lightsabersound;
            Lightsabershow.Play();
            AKrifle = false;
            Lightsaber = true;
        }

        if (AKrifle==true)
        {
            AKRifle.SetActive(true);
            muzzle.SetActive(true);
            LightSaber.SetActive(false);
        }

        if (Lightsaber==true)
        {         
            LightSaber.SetActive(true);
            AKRifle.SetActive(false);
            muzzle.SetActive(false);
        }
	}

    public void onAK()
    {
        equtAK = true;
        eutSword = false;
    }

    public void onSword()
    {
        equtAK = false;
        eutSword = true;
    }
}
