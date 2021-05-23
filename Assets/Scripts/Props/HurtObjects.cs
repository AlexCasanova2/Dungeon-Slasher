using UnityEngine;

public class HurtObjects : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().AddDamage(1);
        }
    }
    
}
