using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject continueButton;
    public AudioSource audioSource;

    //Controllers
    public GameObject mainMenu;
    public GameObject generalMenu, optionsMenu, areYouSureMenu;

    void Start()
    {
        if (SaveManager.instance.exists)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

       /*
        //Clear selected objects
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
       */
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        /*
        //Clear selected objects
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        */

    }

    #region Buttons Functions
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        SaveManager.instance.Load();
    }

    public void NewGame(){
        SceneManager.LoadScene(1);
        SaveManager.instance.DeleteSavedData();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowOptions()
    {
        optionsMenu.SetActive(true);
        generalMenu.SetActive(false);
    }

    public void HideOptions()
    {
        optionsMenu.SetActive(false);
        generalMenu.SetActive(true);
    }

    public void ShowAreYouSure()
    {
        generalMenu.SetActive(false);
        areYouSureMenu.SetActive(true);
    }

    public void HideAreYouSure()
    {
        generalMenu.SetActive(true);
        areYouSureMenu.SetActive(false);
    }

    #endregion

    

    #region PointerEvents


    public void ClickSound()
    {
        audioSource.Play();
    }

    #endregion
}
