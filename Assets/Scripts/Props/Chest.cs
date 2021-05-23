using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool haveGold;
    public int goldToGive;
    public bool specialItem;

    public enum SpecialItem { Key}
    public SpecialItem specialItemToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {
            //Mostar UI para interactuar
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Dar recompensas al player por abrir el cofre

            if (haveGold)
            {
                Debug.Log("Has recibido: " + goldToGive + " de oro");
            }
            else
            {
                //Nada
            }

            if (specialItem)
            {
                if (specialItemToGive == SpecialItem.Key)
                {
                    Debug.Log("Has recibido: una llave");
                }
            }
            else
            {
                //Nada
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {
            //Ocultar UI 
        }
    }

    
}


