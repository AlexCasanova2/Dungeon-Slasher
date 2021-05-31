using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().playerSpeed = 1;
            collision.GetComponent<PlayerController>().jumpForce = 5;
            Debug.Log("Entra el player");
            collision.GetComponent<PlayerController>().isWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().playerSpeed = 2.5f;
            collision.GetComponent<PlayerController>().jumpForce = 6;
            collision.GetComponent<PlayerController>().isWater = false;
        }
    }
}
