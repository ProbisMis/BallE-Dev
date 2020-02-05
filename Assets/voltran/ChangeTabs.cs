using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTabs : MonoBehaviour
{
    public GameObject profilePage;
    public GameObject leadersPage;
    public GameObject shopPage;
    public GameObject settingsPage;

    public GameObject profileButton;
    public GameObject leadersButton;
    public GameObject shopButton;
    public GameObject settingsButton;
    public Camera cam;
    public GameObject canvas;
    public GameObject profile;

    public Camera main;
    void OnMouseUp()
    {
        if (transform.name == "ProfileButton")
        {
            canvas.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            leadersButton.transform.GetChild(0).gameObject.SetActive(false);
            shopButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsButton.transform.GetChild(0).gameObject.SetActive(false);
            profileButton.transform.GetChild(0).gameObject.SetActive(true);

            leadersPage.SetActive(false);
            shopPage.SetActive(false);
            settingsPage.SetActive(false);
            profilePage.SetActive(true);
            //profilePage.transform.GetChild(5).GetComponent<TextMesh>().text = DBManager.fetchedUser.points.ToString();
        }
        else if (transform.name == "LeadersButton")
        {
            canvas.gameObject.SetActive(true);
            cam.gameObject.SetActive(true);
            profileButton.transform.GetChild(0).gameObject.SetActive(false);
            shopButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsButton.transform.GetChild(0).gameObject.SetActive(false);
            leadersButton.transform.GetChild(0).gameObject.SetActive(true);

            profile.GetComponent<FillLeaderBoard>().CallLeaderList();

            profilePage.SetActive(false);
            shopPage.SetActive(false);
            settingsPage.SetActive(false);
            leadersPage.SetActive(true);
        }
        else if (transform.name == "ShopButton")
        {
            canvas.gameObject.SetActive(true);
            cam.gameObject.SetActive(true);
            profileButton.transform.GetChild(0).gameObject.SetActive(false);
            leadersButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsButton.transform.GetChild(0).gameObject.SetActive(false);
            shopButton.transform.GetChild(0).gameObject.SetActive(true);

            profilePage.SetActive(false);
            leadersPage.SetActive(false);
            settingsPage.SetActive(false);
            shopPage.SetActive(true);
        }
        else if (transform.name == "SettingsButton")
        {
            canvas.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            profileButton.transform.GetChild(0).gameObject.SetActive(false);
            leadersButton.transform.GetChild(0).gameObject.SetActive(false);
            shopButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsButton.transform.GetChild(0).gameObject.SetActive(true);

            profilePage.SetActive(false);
            leadersPage.SetActive(false);
            shopPage.SetActive(false);
            settingsPage.SetActive(true);
        }
        else if (transform.name == "ReturnButton")
        {
            canvas.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            leadersButton.transform.GetChild(0).gameObject.SetActive(false);
            shopButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsButton.transform.GetChild(0).gameObject.SetActive(false);
            profileButton.transform.GetChild(0).gameObject.SetActive(true);

            leadersPage.SetActive(false);
            shopPage.SetActive(false);
            settingsPage.SetActive(false);
            profilePage.SetActive(true);
            main.GetComponent<CameraPosition>().ChangeCamPos(0);
        }
    }
}
