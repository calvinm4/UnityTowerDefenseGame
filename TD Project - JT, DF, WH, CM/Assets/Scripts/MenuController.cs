using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    // Outlets
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject settingsMenu;
    public GameObject secretMenu;
    public GameObject background;
    public bool isPaused;
    public bool audioPaused;
    public float gameSpeed;

    // Methods
    void Awake()
    {
        instance = this;
        gameSpeed = 1f;
        Hide();
    }

    public void Show()
    {
        ShowMainMenu();
        gameObject.SetActive(true);
        Camera.main.GetComponent<AudioSource>().Pause();
        Time.timeScale = 0;
        background.SetActive(true);
        PlaceMonster.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        background.SetActive(false);
        if (audioPaused)
        {
            Camera.main.GetComponent<AudioSource>().Pause();
            AudioListener.volume = 0;
        }
        else
        {
            Camera.main.GetComponent<AudioSource>().UnPause();
            AudioListener.volume = 1;
        }
        Time.timeScale = gameSpeed;
        PlaceMonster.isPaused = false;
    }

    void SwitchMenu(GameObject someMenu)
    {
        // Turn off all menus
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        secretMenu.SetActive(false);

        // Turn on requested menu
        someMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        SwitchMenu(mainMenu);
    }

    public void ShowOptionsMenu()
    {
        SwitchMenu(optionsMenu);
    }

    public void ShowSettingsMenu()
    {
        SwitchMenu(settingsMenu);
    }

    public void ShowSecretMenu()
    {
        SwitchMenu(secretMenu);
    }

    //public void ResetScore()
    //{
    //    PlayerPrefs.DeleteKey("Score");
    //    PlayerController.instance.score = 0;
    //}

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void SpeedNormal()
    {
        gameSpeed = 1f;
        //Hide();
    }
    public void Speed2x()
    {
        gameSpeed  = 2f;
        // MoveEnemy.instance.speed = 2f;
        //Hide();
        
    }
    public void Speed3x()
    {
        gameSpeed = 3f;
        // MoveEnemy.instance.speed = 3f;
        //Hide();
        
    }
    public void Speed5x()
    {
        gameSpeed = 5f;
        // MoveEnemy.instance.speed = 5f;
        //Hide();
    }

    public void Mute()
    {
        audioPaused = !audioPaused;
    }

    // Start is called before the first frame update
    void Start()
    {
        //isPaused = false;
        audioPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPaused)
        //{
        //    Show();
        //}
        //else
        //{
        //    Hide();
        //}
    }
}
