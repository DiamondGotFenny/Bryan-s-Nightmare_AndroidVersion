using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyControl : MonoBehaviour {

    [SerializeField] Toggle easyToggle;
    [SerializeField] Toggle normalToggle;
    [SerializeField] Toggle hardToggle;

    GameObject enemyManager;
    EnemyManager01 enemyManager01;

    GameObject[] enemies;

    bool easyModeOn=false;
    bool normalModeOn = false;
    bool hardModeOn = false;
    void Awake()
    {

    }

    // Use this for initialization
    void Start () {
       SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void OnEasyMode()
    {
        hardModeOn = false;
        normalModeOn = false;
        easyModeOn = true;
       
    }

    public void OnNormalMode()
    {
        hardModeOn = false;
        normalModeOn = true;
        easyModeOn = false;
        
    }

    public void OnHardMode()
    {
        hardModeOn = true;
        normalModeOn = false;
        easyModeOn = false;
        
    }

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        modecontrol();
    }

    void modecontrol()
    {
        if (hardModeOn)
        {
            PlayerPrefs.DeleteKey(GameEvent.difficultyNUM);
            PlayerPrefs.SetInt(GameEvent.difficultyNUM, 300);
        }
        if (normalModeOn)
        {
            PlayerPrefs.DeleteKey(GameEvent.difficultyNUM);
            PlayerPrefs.SetInt(GameEvent.difficultyNUM, 200);
        }
        if (easyModeOn)
        {
            PlayerPrefs.DeleteKey(GameEvent.difficultyNUM);
            PlayerPrefs.SetInt(GameEvent.difficultyNUM, 100);
        }
    }
}
