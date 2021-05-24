using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Instancia de la clase
    public static PlayerController inst;

    [Header("Player Attributes")]
    public float playerSpeed = 2.5f;
    public float jumpForce = 2.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0;
    [Header("Player Colliders")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    Vector2 respawnPoint;
    [HideInInspector] public bool canRespawn;

    //AudioClips
    [HideInInspector] public AudioClip footstep1;
    [HideInInspector] public AudioClip footstep2;
    [HideInInspector] public AudioClip landing;

    //References
    Rigidbody2D rb;
    Animator anim;
    AudioSource audioSource;
    PlayerHealth playerHealth;

    //Movement
    Vector2 movement;
    bool facingRight = true;
    bool isGrounded;
    bool _isHitted;

    //NEW INPUT MOVEMENT
    float inputX;
    bool jumPressed;

    //Hang Time for Jump
    public float hangTime = .2f;
    public float hangCounter;

    public float jumpBufferLength = .1f;
    float jumpBufferCount;

    //Attack
    bool isAttacking;

    private void Awake()
    {
        if (inst == null)
        {

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Destroy(gameObject);
            }
            else
            {
                //Primera instancia
                PlayerController.inst = this;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        respawnPoint = transform.position;

        //Save system
        if (SaveManager.instance.hasLoaded)
        {
            respawnPoint = SaveManager.instance.activeSave.respawnPosition;
            transform.position = respawnPoint;
        }

    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }

        if (isAttacking == false)
        {
            #region Eliminar ?
            //START OLD SYSTEM INPUT

            /*
            //Saber si el usuario se mueve a izquierda o derecha (-1 o 1)
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontalInput, 0f);

            //Dar la vuelta al character
            //Si el personaje quiere mirar a la izquierda y esta mirando a la derecha
            if (horizontalInput < 0f && facingRight == true)
            {
                Flip();
                //Si el personaje quiere mirar a la derecha y esta mirando a la izquierda
            }
            else if (horizontalInput > 0f && facingRight == false)
            {
                Flip();
            }
            */
            //END OLD INPUT SYSTEM

            #endregion

            movement = new Vector2(inputX, 0f);
            if (inputX < 0f && facingRight == true)
            {
                Flip();
            }
            else if (inputX > 0f && facingRight == false)
            {
                Flip();
            }
        }
        //Is grounded?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Manage HangTime
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        //Is hitted
        _isHitted = gameObject.GetComponent<PlayerHealth>().isHitted;
    }

    private void FixedUpdate()
    {
        if (isAttacking == false)
        {
            //Moviendo objetos fisicos (movement.normalized.x = si se mueve izquierda o derecha)
            float horizontalVelocity = movement.normalized.x * playerSpeed;
            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        }
    }

    private void LateUpdate()
    {
        //Por ultimo cambiamos el valor del animator 
        anim.SetBool("Idle", movement == Vector2.zero);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("VerticalVelocity", rb.velocity.y);

        //Animator
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hitted"))
        {
            _isHitted = true;
        }
        else
        {
            _isHitted = false;
        }

        if (canRespawn)
        {
            Debug.Log("2");
            transform.position = respawnPoint;
            canRespawn = false;
            playerHealth.playerIsDead = false;
            return;
        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            transform.position = respawnPoint;
            playerHealth.AddDamage(1);
        }
        if (collision.CompareTag("CheckPoint"))
        {
            respawnPoint = collision.transform.position;

            SaveManager.instance.activeSave.respawnPosition = collision.transform.position;

            SaveManager.instance.Save();
        }
    }

    //NEW SYSTEM INPUT
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    } 
    public void Jump(InputAction.CallbackContext context)
    {
        jumpBufferCount = jumpBufferLength;

        if (context.performed && isGrounded && jumpBufferCount >= 0 && hangCounter > 0f && !isAttacking)
        {
            isGrounded = false;
            hangCounter = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpBufferCount = 0;
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
            jumpBufferCount -= Time.deltaTime;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Attack");
            audioSource.Play();
            CinemachineShake.inst.ShakeCamera(2f, .1f);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
        }
        if (context.canceled)
        {
            Debug.Log("Cancelled");
        }
    }

    #region OnLevelLoad
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        /*
         if (scene.name == "MainMenu" && PlayerController.inst == null)
        {
            Destroy(gameObject);
        }
        else if (PlayerController.inst != null)
        {
            PlayerController.inst = this;
        }
        */
    }
    #endregion

    #region PlayerSounds
    public void FirstFootstep()
    {
        audioSource.PlayOneShot(footstep1, 0.2f);
    }
    public void SecondFootstep()
    {
        audioSource.PlayOneShot(footstep2, 0.2f);
    }

    public void LandingSound()
    {
        audioSource.PlayOneShot(landing, 0.2f);
    }

    #endregion
}
