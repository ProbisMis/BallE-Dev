using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMenu : MonoBehaviour
{
    public Camera main;
    public GameObject canvas;
    //public Menu menu;

    //public GameObject canvas;


    void OnMouseDown()
    {
        if (!CameraPosition.camMoving)
        {
            if (transform.name == "StartButton")
                main.GetComponent<CameraPosition>().ChangeCamPos(0);
            //Debug.Log("CAM NOT MOVING");
            else
                main.GetComponent<CameraPosition>().ChangeCamPos(11);

            if (transform.gameObject.name == "GoToMenu")
            {
                DBManager.Instance.LogoutUser();
                Menu.userLoggedin = false;
                canvas.SetActive(true);
            }
            else
            {
                DBManager.Instance.DeleteUser();
            }
        }


    }

}
