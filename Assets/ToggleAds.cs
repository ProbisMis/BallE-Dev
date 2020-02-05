using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAds : MonoBehaviour
{

    public static bool adsToggle = true;
    void OnMouseDown()
    {
        if (adsToggle)
        {
            adsToggle = false;
            Debug.Log("NO ADS");
        }
        else
        {
            adsToggle = true;
            Debug.Log("FUCKING ADS ARE ON");
        }
    }
}
