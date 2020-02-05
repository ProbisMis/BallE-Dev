using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!LevelCreator.editMap)
            transform.Rotate(0, 0, 3);
    }
}
