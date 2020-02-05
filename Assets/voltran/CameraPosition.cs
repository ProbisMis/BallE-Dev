using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public static bool camMoving;
    Vector3 newPos;
    public void ChangeCamPos(float xpos)
    {
        if (!camMoving)
        {
            camMoving = true;
            newPos = new Vector3(xpos, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        if (camMoving)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 4.5f);
            if (Vector3.Distance(transform.position, newPos) <= 0.05f)
            {
                transform.position = newPos;
                camMoving = false;
            }
        }
    }
}
