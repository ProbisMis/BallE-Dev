using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{

    public AudioManager audioManager;

    void OnMouseDown()
    {
        if(transform.name =="SoundButton")
        {
            if(AudioManager.soundstatus)
            {
                audioManager.SoundButton();
            }
            else
            {
                audioManager.SoundButton();
            }
        }
        else if (transform.name == "MusicButton")
        {
            if (AudioManager.musicstatus)
            {
                audioManager.MusicButton();
            }
            else
            {
                audioManager.MusicButton();
            }
        }
    }
}
