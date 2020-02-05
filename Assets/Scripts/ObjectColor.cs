using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = LevelCreator.objColor;
    }

}
////---Good colors
//0.12f, 0.14f, 0.5f
//RGBA(0.232, 0.345, 0.673, 1.000)
//RGBA(0.096, 0.257, 0.785, 1.000)
//RGBA(0.928, 0.755, 0.234, 1.000)
//RGBA(0.902, 0.423, 0.381, 1.000)
//RGBA(0.222, 0.849, 0.808, 1.000)
//RGBA(0.105, 0.586, 0.953, 1.000)