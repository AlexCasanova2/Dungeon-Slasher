using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject continueButton;
    public AudioSource audioSource;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;
    public GameObject image8;

    //Controllers
    public GameObject mainMenu;
    public GameObject generalMenu, optionsMenu;
    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

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

        //Clear selected objects
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

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
        //Clear selected objects
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);


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

    #endregion

    #region Controllers Gamepad
    
    #endregion

    #region PointerEvents


    public void ClickSound()
    {
        audioSource.Play();
    }

    public void TestPointerEnter()
    {
        Debug.Log(transform.name);
    }


    public void OnPointerEnterFirstButton()
    {
        image1.SetActive(true);
        image2.SetActive(true);
        audioSource.Play();

    }
    public void OnPointerExitFirstButton()
    {
        image1.SetActive(false);
        image2.SetActive(false);
    }

    public void OnPointerEnterSecondButton()
    {
       image3.SetActive(true);
       image4.SetActive(true);
       audioSource.Play();
    }
    public void OnPointerExitSecondButton()
    {
        image3.SetActive(false);
        image4.SetActive(false);
    }
    public void OnPointerEnterThirdButton()
    {
        image5.SetActive(true);
        image6.SetActive(true);
        audioSource.Play();
    }
    public void OnPointerExitThirdButton()
    {
        image5.SetActive(false);
        image6.SetActive(false);
    }
    public void OnPointerEnterFourthButton()
    {
        image7.SetActive(true);
        image8.SetActive(true);
        audioSource.Play();
    }
   
    public void OnPointerExitFourthButton()
    {
        image7.SetActive(false);
        image8.SetActive(false);
    }

    public void OnPointerClick()
    {

    }

    #endregion
}
