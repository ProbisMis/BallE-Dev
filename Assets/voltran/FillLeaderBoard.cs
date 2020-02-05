using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Models;
public class FillLeaderBoard : MonoBehaviour
{
    public GameObject[] BoardPages;
    public DBManager db;
    public GameObject info;
    //public Texture2D LWRemptyavatar;
    //public GameObject userInfo;
    int ondalık;

    public void CallLeaderList()
    {
        db.Leaderboard(SetLeaderList);
    }

    void SetLeaderList()
    {

        for (int i = 0; i < DBManager.leaderboard.success.Length; i++)
        {

            if (i < 10)
                ondalık = 0;
            else if (i < 20)
                ondalık = 1;
            else if (i < 30)
                ondalık = 2;
            else if (i < 40)
                ondalık = 3;
            else if (i < 50)
                ondalık = 4;
            else if (i < 60)
                ondalık = 5;
            else if (i < 70)
                ondalık = 6;
            else if (i < 80)
                ondalık = 7;
            else if (i < 90)
                ondalık = 8;
            else if (i < 100)
                ondalık = 9;

            //if (int.Parse(tempArr[2]) != 0)
            //    BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(0).GetComponent<TextMesh>().text = tempArr[0] + ".";
            //else
            //    BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(0).GetComponent<TextMesh>().text = "-";

            BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(2).GetComponent<TextMesh>().text = DBManager.leaderboard.success[i].username;
            BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(3).GetComponent<TextMesh>().text = DBManager.leaderboard.success[i].points.ToString();
            //BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(1).GetComponent<TextMesh>().text = "fucks";
            //BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(2).GetComponent<TextMesh>().text = "fucks2";
            BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(4).GetComponent<TextMesh>().text = DBManager.leaderboard.success[i].normal_highest.ToString();

            //BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(1).GetChild(0).GetComponent<Renderer>().material.mainTexture = CharacterManager.Instance.getCharacterById(int.Parse(tempArr[3])).character_icon_txt;
            //BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(4).GetComponent<TextMesh>().text = tempArr[2];
            //BoardPages[ondalık].transform.GetChild(i - ondalık * 10).GetChild(3).GetComponent<TextMesh>().text = RankManager.Instance.findNameWithID(int.Parse(tempArr[4]));


        }

        if (DBManager.leaderboard.myRank != 0)
        {
            //Debug.Log("info");
            info.transform.GetChild(0).GetComponent<TextMesh>().text = DBManager.leaderboard.myRank.ToString();
            info.transform.GetChild(2).GetComponent<TextMesh>().text = DBManager.fetchedUser.username;
            info.transform.GetChild(3).GetComponent<TextMesh>().text = DBManager.fetchedUser.points.ToString();
            info.transform.GetChild(4).GetComponent<TextMesh>().text = DBManager.fetchedUser.normal_highest.ToString();
        }
        else
        {
            info.transform.GetChild(0).GetComponent<TextMesh>().text = "-";
            info.transform.GetChild(2).GetComponent<TextMesh>().text = "-";
            info.transform.GetChild(3).GetComponent<TextMesh>().text = "-";
            info.transform.GetChild(4).GetComponent<TextMesh>().text = "-";
        }
    }




}