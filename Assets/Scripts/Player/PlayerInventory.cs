using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int goldAmount;
    public int potionAmount;

    //References    
    TextMeshProUGUI goldText;
    TextMeshProUGUI potionText;

    GameObject _goldText;
    GameObject _potionText;

    void Awake()
    {
        
    }

    void Start()
    {
        //Save system
        if (SaveManager.instance.hasLoaded)
        {
            goldAmount = SaveManager.instance.activeSave.gold;
            potionAmount = SaveManager.instance.activeSave.potions;
        }
        else
        {
            SaveManager.instance.activeSave.gold = goldAmount;
            SaveManager.instance.activeSave.potions = potionAmount;
        }

    }

    void Update()
    {
        goldText.SetText(goldAmount.ToString());
        potionText.SetText(potionAmount.ToString());
    }

    #region OnLevelLoad
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //CODIGO DEL AWAKE

        _goldText = GameObject.Find("GoldText");
        _potionText = GameObject.Find("PotionText");

        goldText = _goldText.GetComponent<TextMeshProUGUI>();
        potionText = _potionText.GetComponent<TextMeshProUGUI>();
    }
    #endregion

}
