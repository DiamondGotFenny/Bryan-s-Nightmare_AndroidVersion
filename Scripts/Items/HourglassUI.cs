using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HourglassUI : MonoBehaviour {
    #region Singleton
    private static HourglassUI _instance;

    public static HourglassUI Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = GameObject.FindObjectOfType<HourglassUI>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
#endregion 

    public int startHGNumber=0;
    public int maxHGNumber;
    public int currentHGNumber;

    AudioSource HGAudio;

    [SerializeField] Text hourglassDisplay;

    //GameObject bossclone;
    private void Awake()
    {
        HGAudio = GetComponent<AudioSource>();
        Messenger.AddListener(GameEvent.ADD_HOURGLASS, AddGlass);

#region SingletonRun
        if (_instance==null)
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
        Messenger.RemoveListener(GameEvent.ADD_HOURGLASS, AddGlass);
    }
    // Use this for initialization
    void Start () {
        startHGNumber = currentHGNumber;

        
    }
	
	// Update is called once per frame
	void Update () {
        hourglassDisplay.text = currentHGNumber.ToString();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            Destroy(this.gameObject);
        }
    }

    
    private void AddGlass()
    {
        currentHGNumber++;
        HGAudio.Play();
        Messenger<int>.Broadcast(GameEvent.Current_HOURGLASS,currentHGNumber);
        if (currentHGNumber > maxHGNumber)
        {
            currentHGNumber = maxHGNumber;
        }
    }
}
