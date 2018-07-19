using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    #endregion

    AudioSource musicPlayer;
    AudioSource entrySFX;

    public AudioClip Level01Music;
    public AudioClip Level02Music;
    public AudioClip Level03Music;
    public AudioClip MainMenuMusic;
    public AudioClip winningmusic;
    public AudioClip lostmusic;

    bool playerWin;
    bool playerDead;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.LevelChange, LevelChangeSFX);
        Messenger<bool>.AddListener(GameEvent.Win_GAME, OnPlayerWin);
        Messenger<bool>.AddListener(GameEvent.PLAYER_DEAD, OnPlayerLost);

        #region SingletonRun
        if (_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if ( this !=_instance)
            {
                Destroy(this.gameObject);
            }
        }
        #endregion        
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LevelChange, LevelChangeSFX);
        Messenger<bool>.RemoveListener(GameEvent.Win_GAME, OnPlayerWin);
        Messenger<bool>.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerLost);
    }


    // Use this for initialization
    void Start () {
        AudioSource[] Sound = GetComponents<AudioSource>();
        musicPlayer = Sound[0];
        entrySFX = Sound[1];
        Time.timeScale = 1;
        AudioListener.pause = false;
        playerWin = false;
        playerDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        levelMusicPlay();
    }

    void levelMusicPlay()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (playerDead == true)
        {
            if (lostmusic != null)
            {
                musicPlayer.clip = lostmusic;
                if (musicPlayer.isPlaying == false)
                {
                    musicPlayer.Play();
                }
            }
        }

        if (playerWin == true)
        {
            if (winningmusic != null)
            {
                musicPlayer.clip = winningmusic;
                if (musicPlayer.isPlaying == false)
                {
                    musicPlayer.Play();
                }
            }
        }

        if (currentScene.name== "MainMenu"&&playerWin==false&&playerDead==false)
        {
            Time.timeScale = 1;
            if (MainMenuMusic != null)
            {
                musicPlayer.clip = MainMenuMusic;
                if (musicPlayer.isPlaying == false)
                {
                    musicPlayer.Play();
                }
            }
        }

        if (currentScene.name== "Level 01" && playerWin == false && playerDead == false)
        {
            if (Level01Music != null)
            {
                musicPlayer.clip = Level01Music;
                if (musicPlayer.isPlaying == false)
                {
                    musicPlayer.Play();
                }              
            }           
        }

        if (currentScene.name == "Level 02" && playerWin == false && playerDead == false)
        {
            if (Level02Music != null)
            {
                musicPlayer.clip = Level02Music;
                if (musicPlayer.isPlaying == false)
                {
                    musicPlayer.Play();
                }
            }
        }
        if (currentScene.name == "Level 03" && playerWin == false && playerDead == false)
        {
            if (Level03Music != null)
            {
                musicPlayer.clip = Level03Music;
                if (musicPlayer.isPlaying == false)
                {
                    musicPlayer.Play();
                }
            }        
        }
    }

    void LevelChangeSFX()
    {
        entrySFX.Play();
    }

    void OnPlayerWin(bool wingame)
    {
         playerWin = wingame;
    }
    void OnPlayerLost(bool isplayerdead)
    {
        playerDead = isplayerdead;
    }
}
