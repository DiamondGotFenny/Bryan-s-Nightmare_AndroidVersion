using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour {

    [SerializeField] GameObject difficultyControl;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject CClaims;

    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Dropdown resulotionSelection;
    [SerializeField] Slider musicSlider;
    [SerializeField] Toggle normalToggle;
    public Resolution[] resulotions;


    private void OnEnable()
    {
        resulotionSelection.onValueChanged.AddListener(delegate { OnResulotionChange(); });
        resulotions = Screen.resolutions;
        foreach (Resolution resolution in resulotions)
        {
            resulotionSelection.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }

    // Use this for initialization
    void Start () {
      musicSlider.value= PlayerPrefs.GetFloat(GameEvent.Music_Volume, 0f);
        normalToggle.isOn = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnOptionMenu()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
        CClaims.SetActive(false);
    }

    public void OnHTPMenu()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(false);
        CClaims.SetActive(false);
    }

    public void OnMainMenu()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        CClaims.SetActive(false);
    }

    public void OnCClaims()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(false);
        CClaims.SetActive(true);
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnResulotionChange()
    {
        Screen.SetResolution(resulotions[resulotionSelection.value].width, resulotions[resulotionSelection.value].height, Screen.fullScreen);
    }
}
