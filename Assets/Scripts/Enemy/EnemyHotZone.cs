using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHotZone : MonoBehaviour
{
    EnemyPatrol enemyPatrol;
    bool inRange;
    Animator anim;

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            enemyPatrol.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerHealth>().playerIsDead)
        {
            inRange = true;
        }
        else if(collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerHealth>().playerIsDead)
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyPatrol.triggerArea.SetActive(true);
            enemyPatrol.inRange = false;
            enemyPatrol.SelectTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyPatrol.triggerArea.SetActive(true);
            enemyPatrol.inRange = false;
            enemyPatrol.SelectTarget();
        }
    }
}
