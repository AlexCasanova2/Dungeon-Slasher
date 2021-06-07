using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Instancia de la clase
    public static GameController inst;
    GameObject vCam;
    GameObject cameraConfiner;
    private PlayerHealth playerHealth;
    bool _playerIsDead;
    


    private void Awake()
    {
        if (inst == null)
        {
            GameController.inst = this;
        }
        else
        {
            //Ya existe una instancia
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        cameraConfiner = GameObject.Find("CameraConfiner");
        vCam = GameObject.Find("CM vcam1");

        if (vCam == null || cameraConfiner == null)
        {
            //Debug.Log("No Existe la camara");
        }
        else
        {
            //Debug.Log("La camara no existe");
            vCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = cameraConfiner.GetComponent<PolygonCollider2D>();
        }
    }
    private void Start()
    {
        
    }
    void Update()
    {
       
    }


    public void PlayerDead()
    {
        if (_playerIsDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Has muerto");
            return;
        }
    }

    #region LoadScene

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //CODIGO NATIVO
        cameraConfiner = GameObject.Find("CameraConfiner");
        vCam = GameObject.Find("CM vcam1");


        if (vCam ==null || cameraConfiner == null)
        {
            //Debug.Log("ES NULL");
        }
        else
        {
            vCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = cameraConfiner.GetComponent<PolygonCollider2D>();
            //Debug.Log("NO ES NULL Y LO ASIGNO");
        }
        
    }
    #endregion
}
