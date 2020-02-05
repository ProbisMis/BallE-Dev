using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUITexts : MonoBehaviour
{
    public GameObject profilePage;

    // Update is called once per frame
    void OnMouseDown()
    {
        if (DBManager.fetchedUser != null)
        {
            profilePage.transform.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = DBManager.fetchedUser.points.ToString();
            profilePage.transform.GetChild(4).GetChild(0).GetComponent<TextMesh>().text = DBManager.fetchedUser.normal_highest.ToString();

        }

    }
}
