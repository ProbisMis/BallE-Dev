using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToProfile : MonoBehaviour
{
    public Camera main;


    void OnMouseDown()
    {
        if (!CameraPosition.camMoving)
        {
            main.GetComponent<CameraPosition>().ChangeCamPos(-11);
        }
    }
}
