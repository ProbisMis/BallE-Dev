using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<TextMesh>().text != "Level  " + Player.level)
        {
            GetComponent<TextMesh>().text = "Level  " + Player.level;
        }
    }
}
