using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    int maxHealth = 5;
    [HideInInspector] public bool isHitted;

    Animator anim;
    AudioSource audioSource;

    private void Start()
    {
        enemyHealth = maxHealth;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void AddDamage(int amount)
    {
        enemyHealth -= amount;
        anim.SetTrigger("GotDamage");
        audioSource.Play();

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            anim.SetBool("Dead", true);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hitted"))
        {
            isHitted = true;
        }
        else
        {
            isHitted = false;
        }
    }

}
