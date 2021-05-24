using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;


public class BasicChest : MonoBehaviour
{

    //References
    public ChestTest chest;
    public Image imageToShow;
    public TextMeshProUGUI rewardText;
    [HideInInspector] public GameObject rewardPanel;
    [HideInInspector] public GameObject interactPanel;
    public string textToShow;
    public TextMeshProUGUI interactText;
    public GameObject itemYoActivateInventory;
    GameObject child;
    Light2D lt;
    PlayerController playerController;

    //Save System
    bool isOpened;

    private void Start()
    {
        child = gameObject.transform.GetChild(1).gameObject;
        lt = child.GetComponent<Light2D>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Mostar UI para interactuar
            interactPanel.SetActive(true);
            interactText.SetText(textToShow);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOpened = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            child.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Dar recompensas al player por abrir el cofre

            if (chest.haveGold)
            {
                Debug.Log("Has recibido: " + chest.goldToGive + " de oro");
                collision.gameObject.GetComponent<PlayerInventory>().goldAmount += chest.goldToGive;
                lt.intensity = 0.5f;
                StartCoroutine(Smooth());

                SaveManager.instance.activeSave.gold += chest.goldToGive;
            }
            else
            {
                //Nada
            }

            if (chest.haveSpecialItem)
            {
                Debug.Log(chest.specialItemToGive);
                rewardPanel.SetActive(true);
                imageToShow.sprite = chest.sprite;
                rewardText.SetText(chest.rewardText);
                rewardPanel.GetComponent<Animator>().SetBool("start", true);
                StartCoroutine(Wait());
            }
            else
            {
                //Nada
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Ocultar UI 
            interactPanel.SetActive(false);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        rewardPanel.GetComponent<Animator>().SetBool("end", true);
        itemYoActivateInventory.SetActive(true);
    }

    IEnumerator Smooth()
    {
        yield return new WaitForSeconds(2f);
        lt.intensity = 0f;
    }
}
