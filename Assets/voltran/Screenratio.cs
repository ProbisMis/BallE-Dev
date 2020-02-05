using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenratio : MonoBehaviour
{
    float defaultRatio;
    float currentRatio;

    Vector3 defaultScale;
    Vector3 currentScale;

    void Start()
    {
        float defaultH = 1920;
        float defaultW = 1080;

        float w = Screen.width;
        float h = Screen.height;
        
        float ratioW = w / defaultW;
        float ratioH = h / defaultH;

        float realRatio;

        if (ratioW > ratioH)
        {
            realRatio = ratioW;
        }
        else
            realRatio = ratioH;

        defaultScale = transform.localScale;
        currentScale = new Vector3(defaultScale.x * realRatio, defaultScale.y * realRatio, defaultScale.z * realRatio);
    
        transform.localScale = currentScale;
    }


}
