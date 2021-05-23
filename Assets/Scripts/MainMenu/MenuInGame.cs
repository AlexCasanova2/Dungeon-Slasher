using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public static MenuInGame inst;

    GameObject player;
    bool canMove;

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public static bool GameIsPaused = false;

    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

    //Referencia al Controller

    private void Awake()
    {
        if (MenuInGame.inst == null)
        {
            MenuInGame.inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameIsPaused = false;
        player = GameObject.FindWithTag("Player");
        canMove = true;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }
        else
        {
            MenuInGame.inst = this;
        }

        if (Input.GetButtonDown("MainMenu"))
        {
            PauseResume();
        }
    }
    
    public void PauseResume()
    {
        pauseMenu.SetActive(!pauseMenu.active);
        canMove = !canMove;
        player.transform.GetComponent<PlayerController>().enabled = canMove;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            
            //Clear selected objects
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        GameIsPaused = !GameIsPaused;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        player.transform.GetComponent<PlayerController>().enabled = true;
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);

        //Clear selected objects
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);

        //Clear selected objects
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void SaveGame()
    {
        SaveManager.instance.Save();
    }


    #region OnLevelLoad
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

    }
    #endregion

}
