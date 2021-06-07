using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerController playerController;
    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    PlayerInventory playerInventory;

    //Control Variables
    public bool canInteract;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    
    void Update()
    {
        PlayerIsDead();
    }


    public void PlayerIsDead()
    {
        if (playerHealth.playerIsDead)
        {
            //Puede hacer respawn
            playerController.canRespawn = true;
            playerHealth.health = playerHealth.maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact"))
        {
            if (canInteract)
            {
                collision.gameObject.GetComponent<BasicChest>().open = true;
                return;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
