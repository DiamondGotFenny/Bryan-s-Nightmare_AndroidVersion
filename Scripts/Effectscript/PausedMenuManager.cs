using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PausedMenuManager : MonoBehaviour {

    #region Singleton
    private static PausedMenuManager _instance;
    public static PausedMenuManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PausedMenuManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] AudioMixer masterMixer;

    private AudioMixerSnapshot paused;
    private AudioMixerSnapshot unpaused;
    int add_scores;
    float timecount;
    public string highscorePos;
    string recordTime;
    public int temp;

    public GameObject pausedMenu;
    public GameObject WinningPannel;
    public GameObject LostPannel;
    public GameObject controllPannel;

    [SerializeField] string Levelname;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Text scores;
    [SerializeField] Text[] finalScores;
    [SerializeField] Toggle isMute;

    bool gamecomplete;
    bool _pause;

   

    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.ADD_SCORE, addScores);
        Messenger<bool>.AddListener(GameEvent.Win_GAME, WinGame);
        Messenger<bool>.AddListener(GameEvent.PLAYER_DEAD, OnLost);
        #region SingletonRun
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
        #endregion        
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.ADD_SCORE, addScores);
        Messenger<bool>.RemoveListener(GameEvent.Win_GAME, WinGame);
        Messenger<bool>.RemoveListener(GameEvent.PLAYER_DEAD, OnLost);
    }

    // Use this for initialization
    void Start () {
        // pausedMenu.enabled = false;
        controllPannel = GameObject.FindGameObjectWithTag("Controlpannel");
        _pause = false;
        pausedMenu.SetActive(false);
        WinningPannel.SetActive(false);
        LostPannel.SetActive(false);
        gamecomplete = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        paused = masterMixer.FindSnapshot("Paused");
        unpaused = masterMixer.FindSnapshot("Unpaused");

        musicSlider.value = PlayerPrefs.GetFloat(GameEvent.Music_Volume, 0f);
        sfxSlider.value= PlayerPrefs.GetFloat(GameEvent.SFX_Volume, 0f);

        add_scores = 0;
    }
	
	// Update is called once per frame
	void Update () {
        OnpauseMenu();
      
        scores.text = "Score: " + add_scores;      
 
    }

    //public void Paused()
    //{
    //    //if (pausedMenu.enabled == true)
    //    //{
    //    //    Time.timeScale = 0;
    //    //}
    //    //else
    //    //{
    //    //    Time.timeScale = 1;
    //    //}
    //    //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    //    //Lowpass();
    //}

    public void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(0.01f);
        }
        else
        {
            unpaused.TransitionTo(0.01f);
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Levelname);
        Time.timeScale = 1;
        Lowpass();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject enemies = GameObject.FindGameObjectWithTag("Enemy");
        Destroy(player);
        Destroy(enemies);
        Messenger<bool>.Broadcast(GameEvent.Win_GAME, false);
        Messenger<bool>.Broadcast(GameEvent.PLAYER_DEAD, false);
        //resume();
        // contiueGame();
        Destroy(controllPannel);
        Destroy(this.gameObject);
    }

    //public void resume()
    //{
    //   // pausedMenu.enabled = false;
    //    Time.timeScale = 1;
    //  //  Lowpass();
    //}

    void pausedGame()
    {
        pausedMenu.SetActive(true);
        Time.timeScale = 0;
       Lowpass();
    }

    public void contiueGame()
    {
        pausedMenu.SetActive(false);
        Time.timeScale = 1;
        Lowpass();
    }

    void OnpauseMenu()
    {
        if (_pause==true)
        {
            //pausedMenu.enabled = !pausedMenu.enabled;
            // Paused();

            if (!pausedMenu.activeInHierarchy)
            {
                pausedGame();
            }

            //else
            //{
            //    contiueGame();
            //}
        }
    }

    void addScores(int score)
    {
        add_scores += score;
    }

    void OnLevelCompleted()
    {
        if (gamecomplete)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (PlayerPrefs.GetInt("highscorePos" + i) < add_scores)
                {
                    temp = PlayerPrefs.GetInt("highscorePos" + i);
                    PlayerPrefs.SetInt("highscorePos" + i, add_scores);
                    if (i < 5)
                    {
                        int j = i + 1;
                        add_scores = PlayerPrefs.GetInt("highscorePos" + j);
                        PlayerPrefs.SetInt("highscorePos" + j, temp);
                    }
                }
            }
        }     
    }

    void OnDisplay()
    {
        if (gamecomplete)
        {
            for (int i = 1; i <= 5; i++)
            {
                finalScores[i-1].text ="Your score"+" "+"No."+i + ":" + PlayerPrefs.GetInt("highscorePos" + i,0);
            }
        }       
    }

    void WinGame(bool wingame)
    {
        Time.timeScale = 0;
        gamecomplete = wingame;
        WinningPannel.SetActive(wingame);
        OnLevelCompleted();
        OnDisplay();
    }

    void OnLost(bool playerdead)
    {
        Time.timeScale = 0;
        LostPannel.SetActive(playerdead);
    }

    public void isOnMute()
    {
        AudioListener.pause = ! AudioListener.pause;
    }

    public void Onpause()
    {
        _pause = true;
    }

    public void CloseMenu()
    {
        _pause = false;
        contiueGame();
    }
}
