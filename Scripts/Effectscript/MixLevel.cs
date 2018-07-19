using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevel : MonoBehaviour {
    public AudioMixer masterMixer;

    private void Start()
    {

    }

    public void setSfxVol(float sfxlvl)
    {
        masterMixer.SetFloat("sfxVol", sfxlvl);
        PlayerPrefs.SetFloat(GameEvent.SFX_Volume, sfxlvl);
    }

    public void setBGMVol(float musiclvl)
    {
        masterMixer.SetFloat("MusicVol", musiclvl);
        PlayerPrefs.SetFloat(GameEvent.Music_Volume, musiclvl);
    }
}
