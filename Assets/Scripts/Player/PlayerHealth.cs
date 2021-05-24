using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int health;
    [HideInInspector]public bool playerIsDead;
    Animator anim;
    public ParticleSystem damageParticle;
    [HideInInspector]public bool isHitted;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        maxHealth = 4;

    }
    void Start()
    {
        //Save system
        if (SaveManager.instance.hasLoaded)
        {
            health = SaveManager.instance.activeSave.health;
        }
        else
        {
            //health = maxHealth;
            SaveManager.instance.activeSave.health = health;
        }
    }

    void Update() {
        if (playerIsDead)
        {
            Debug.Log("1");
            //playerIsDead = false;
            SaveManager.instance.activeSave.health = health;
        }
    }

    public void AddDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            playerIsDead = true;
            Debug.Log("Game Over");
            anim.SetBool("Idle", false);
            
        }
        SaveManager.instance.activeSave.health = health;
        anim.SetTrigger("Hitted");
        
        damageParticle.Play();
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
