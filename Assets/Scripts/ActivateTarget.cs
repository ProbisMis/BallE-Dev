using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTarget : MonoBehaviour
{
    public static bool targetActivated = false;
    public void Activate()
    {
        targetActivated = true;
        GetComponent<Renderer>().material.color = new Color(0.22f, 0.5f, 0.6f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
