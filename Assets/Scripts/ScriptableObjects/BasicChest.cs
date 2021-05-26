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
     public GameObject rewardPanel;
     public GameObject interactPanel;
    public string textToShow;
    public TextMeshProUGUI interactText;
    public GameObject itemYouActivateInventory;
    GameObject child;
    Light2D lt;
    public bool open;

    //Save System
    bool isOpened;
    public bool interacted;

    private void Start()
    {
        child = gameObject.transform.GetChild(1).gameObject;
        lt = child.GetComponent<Light2D>();
    }
    private void Update()
    {
        if (interacted)
        {
            Interacted();
        }
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


    public void Interacted() 
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        child.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        lt.enabled = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (open && !interacted)
        {
            open = false;
            isOpened = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            child.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (SaveManager.instance.activeSave.gameObjectsInteracted.Contains(gameObject.transform.name))
            {
                Debug.Log("Ya has interactuado con este cofre");
                lt.enabled = false;
            }
            else
            {
                //Si es la primera vez que se interactua
                SaveManager.instance.activeSave.gameObjectsInteracted.Add(gameObject.transform.name);
                Debug.Log(gameObject.transform.name);
                Debug.Log("Añado este cofre a la lista de objetos interactuados");

                //Dar recompensas al player por abrir el cofre
                if (chest.haveGold)
                {
                    Debug.Log("Has recibido: " + chest.goldToGive + " de oro");
                    collision.gameObject.GetComponent<PlayerInventory>().goldAmount += chest.goldToGive;
                    lt.enabled = true;
                    lt.intensity = 0.5f;
                    StartCoroutine(Smooth());

                    SaveManager.instance.activeSave.gold += chest.goldToGive;
                }

                if (chest.haveSpecialItem)
                {
                    rewardPanel.SetActive(true);
                    imageToShow.sprite = chest.sprite;
                    rewardText.SetText(chest.rewardText);
                    rewardPanel.GetComponent<Animator>().SetBool("start", true);
                    StartCoroutine(Wait());

                    if (SaveManager.instance.activeSave.inventoryItems.Contains(chest.specialItemToGive))
                    {
                        Debug.Log("Ya tienes este objeto");
                    }
                    else
                    {
                        SaveManager.instance.activeSave.inventoryItems.Add(chest.specialItemToGive);
                        Debug.Log("Has recibido: " + chest.specialItemToGive + " y se ha añadido a tu inventario");
                    }
                }
                interacted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Ocultar UI 
            interactPanel.SetActive(false);
            open = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        rewardPanel.GetComponent<Animator>().SetBool("end", true);
        itemYouActivateInventory.SetActive(true);
    }

    IEnumerator Smooth()
    {
        yield return new WaitForSeconds(2f);
        lt.intensity = 0f;
    }

}
