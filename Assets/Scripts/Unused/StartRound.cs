using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound : MonoBehaviour
{
    public GameObject obj;
    public static int count = 0;
    private float RoundStartTime;
    void OnMouseUp()
    {
        Debug.Log("Started Round");
        Player.gameOn = true;
        RoundStartTime = Time.time;

    }
    void Update()
    {
        if (Player.gameOn)
        {
            if (count < GetInput.actionOrder.Count)
            {
                if (Time.time - RoundStartTime <= GetInput.actionOrder[count].Duration)
                {
                    //Debug.Log(obj.transform.position + GetInput.actionOrder[count].Direction);
                    //obj.transform.position = Vector2.MoveTowards(obj.transform.position, obj.transform.position + GetInput.actionOrder[count].Direction, Time.deltaTime * 3);
                }
                else
                {
                    //Debug.Log("Next Move");
                    //RoundStartTime = Time.time;
                    //count++;
                }
            }
        }
    }
}
