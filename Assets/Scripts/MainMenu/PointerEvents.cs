using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerEvents : MonoBehaviour
{
    

    public void OnPointerEnter()
    {
        var image = gameObject.transform.GetChild(0);
        var image2 = gameObject.transform.GetChild(2);
        image.gameObject.SetActive(true);
        image2.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        var image = gameObject.transform.GetChild(0);
        var image2 = gameObject.transform.GetChild(2);
        image.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
    }
}
