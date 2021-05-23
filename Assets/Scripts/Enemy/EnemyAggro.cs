using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    EnemyPatrol enemyPatrol;

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerHealth>().playerIsDead == false)
        {
            gameObject.SetActive(false);
            enemyPatrol.target = collision.transform;
            enemyPatrol.inRange = true;
            enemyPatrol.hotZone.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
            enemyPatrol.SelectTarget();
            enemyPatrol.inRange = false;
            enemyPatrol.hotZone.SetActive(false);
        }
    }
}
