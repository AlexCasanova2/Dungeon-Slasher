using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    #region Public Variables
    public float raycastLenght;
    public float attackDistance; //Minimum distance for attack
    public float movementSpeed;
    public float timer; //Timer cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    Animator anim;
    float distance; //Store the distance between enemy and player
    bool attackMode;
    bool cooling; //check if enemy is cooling after attack
    float intTimer;
    bool _isHitted;
    bool isDead;
    #endregion


    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Is hitted
        _isHitted = gameObject.GetComponent<EnemyHealth>().isHitted;
        

        if (!attackMode && target != null)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            SelectTarget();
        }
        
        if (inRange)
        {
            EnemyLogic();
        }
    }

    private void LateUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hitted"))
        {
            _isHitted = true;
            anim.SetBool("Idle", true);
        }
        else
        {
            _isHitted = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dead"))
        {
            StopMovement();
        }

        
    }


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false){
            Attack();
        }
        if (cooling)
        {
            Cooldwon();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("CanWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")){
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        attackMode = true;
        
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
        timer = intTimer; //Reset timer
    }

    void Cooldwon()
    {
        timer -= Time.deltaTime;

        if (timer<= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    IEnumerator WaitDespawn()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            //target = leftLimit;
            Flip();
        }
        else
        {
            target = rightLimit;
            Flip();
        }
        if (isDead)
        {
            target = null;
        }
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }

    public void StopMovement()
    {
        isDead = true;
        attackMode = false;
        cooling = true;
        hotZone.SetActive(false);
        triggerArea.SetActive(false);
        anim.SetBool("CanWalk", false);
        anim.SetBool("Idle", false);
        StartCoroutine(WaitDespawn());
    }
}


