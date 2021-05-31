using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject continueButton;
    public AudioSource audioSource;
    public AudioSource selectAudio;

   

    //Controllers
    public GameObject mainMenu;
    public GameObject generalMenu, optionsMenu, areYouSureMenu, audioPanel, videoPanel;

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
        StartCoroutine(Wait());
        
    }
    public void DeleteSavedData()
    {
        SaveManager.instance.DeleteSavedData();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowOptions()
    {
        optionsMenu.SetActive(true);
        //generalMenu.GetComponent<Animator>().SetBool("HideGeneralMenu", true);
        generalMenu.SetActive(false);
    }

    public void HideOptions()
    {
        optionsMenu.SetActive(false);
        generalMenu.SetActive(true);
        //generalMenu.GetComponent<Animator>().SetBool("ShowGeneralMenu", true);
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

    public void ShowAudioPanel()
    {
        optionsMenu.SetActive(false);
        audioPanel.SetActive(true);
    }
    public void HideAudioPanel()
    {
        optionsMenu.SetActive(true);
        audioPanel.SetActive(false);
    }

    public void ShowVideoPanel()
    {
        optionsMenu.SetActive(false);
        videoPanel.SetActive(true);
    }

    public void HideVideoPanel()
    {
        optionsMenu.SetActive(true);
        videoPanel.SetActive(false);

    }


    #endregion



    #region PointerEvents


    public void ClickSound()
    {
        audioSource.Play();
    }
    public void SelectSound()
    {
        selectAudio.Play();
    }

    #endregion

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
        selectAudio.volume = 0;
    }
}
