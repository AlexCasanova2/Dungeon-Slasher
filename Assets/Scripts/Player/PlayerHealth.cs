using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public int maxHealth = 3;
    public int health;
    [HideInInspector]public bool playerIsDead;
    Animator anim;
    public ParticleSystem damageParticle;
    [HideInInspector]public bool isHitted;
    public GameObject enemyHitbox;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }
    void Start()
    {
        health = maxHealth;

        //Save system
        if (SaveManager.instance.hasLoaded)
        {
            health = SaveManager.instance.activeSave.health;
        }
        else
        {
            SaveManager.instance.activeSave.health = health;
        }
    }

    void Update() {
        if (playerIsDead)
        {
            //gameObject.GetComponent<PlayerController>().enabled = true;
            health = maxHealth;
            Debug.Log("et");
            SaveManager.instance.activeSave.health = health;
        }
    }

    public void AddDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            Debug.Log("Game Over");
            anim.SetBool("Idle", false);
            playerIsDead = true;
            //gameObject.GetComponent<PlayerController>().enabled = false;
        }
        SaveManager.instance.activeSave.health = health;
        anim.SetTrigger("Hitted");
        
        damageParticle.Play();
        //Debug.Log("Te han golpeado, tu vida actual es de: " + health);
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
            Debug.Log("Tienes el máximo de vida");
        }
        Debug.Log("Te has curado, tu vida actual es de: " + health);
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
