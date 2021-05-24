using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerController playerController;
    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    PlayerInventory playerInventory;

    private void Awake()
    {
        
    }

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
            Debug.Log("3");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact"))
        {
            Debug.Log("Interact");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

}
