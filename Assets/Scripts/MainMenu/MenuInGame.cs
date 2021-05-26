using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public static MenuInGame inst;

    AudioSource audioSource;
    GameObject player;
    bool canMove;

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public static bool GameIsPaused = false;

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
        audioSource = GetComponent<AudioSource>();
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

        
    }
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void SaveGame()
    {
        SaveManager.instance.Save();
    }

    public void Escape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseResume();
        }
    }

    public void ClickSound()
    {
        audioSource.Play();
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
