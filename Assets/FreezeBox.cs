using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBox : MonoBehaviour
{
    // Update is called once per frame
    public void BroadcastFreeze()
    {
        gameObject.BroadcastMessage("Freeze");
    }
}
