using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSwipe : MonoBehaviour
{
    public GameObject obj;
    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 direction;
    //private float clickTime;
    public static bool startedMovement = false;

    public static float ballSpeed = 3f;

    void Update()
    {
        if (startedMovement)
        {
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, obj.transform.position + direction, Time.deltaTime * ballSpeed);
            //Debug.Log("Started movement");
        }

    }
    void OnMouseDown()
    {
        startPos = Input.mousePosition;
    }
    void OnMouseDrag()
    {
        GetDirection();
    }
    // void OnMouseUp()
    // {
    //     endPos = Input.mousePosition;
    //     GetDirection();
    // }
    void GetDirection()
    {
        endPos = Input.mousePosition;
        if (Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y))
        {
            if (Vector3.Distance(endPos, startPos) > 10)
                if (endPos.x - startPos.x > 0)
                    direction = Vector2.right;
                else
                    direction = -Vector2.right;
        }
        else
        {
            if (Vector3.Distance(endPos, startPos) > 10)
                if (endPos.y - startPos.y > 1)
                    direction = Vector2.up;
                else
                    direction = -Vector2.up;
        }
        startPos = endPos;

        if (Player.isReverse)
            direction = -direction;

        startedMovement = true;

    }
}
