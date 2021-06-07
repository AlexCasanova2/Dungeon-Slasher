using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
     GameObject canvas;
     GameObject autoSave;


    private void OnEnable()
    {
        canvas = GameObject.Find("Canvas");
        autoSave = canvas.transform.GetChild(5).gameObject;
    }
    private void Awake()
    {
        
    }
    public void SetCheckPoint()
    {
        autoSave.SetActive(true);
        StartCoroutine(setActive());
    }

    IEnumerator setActive()
    {
        yield return new WaitForSeconds(2f);
        autoSave.SetActive(false);
    }
}
