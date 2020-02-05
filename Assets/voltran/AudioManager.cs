using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{


    public static bool soundstatus = true;
    public static bool musicstatus = true;

    public GameObject menuSoundButton;
    public GameObject menuMusicButton;
    public GameObject settingsSoundButton;
    public GameObject settingsMusicButton;

    public void SoundButton()
    {
        if (soundstatus)
        {
            soundstatus = false;
            menuSoundButton.transform.GetChild(0).gameObject.SetActive(false);
            menuSoundButton.transform.GetChild(1).gameObject.SetActive(true);
            settingsSoundButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsSoundButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            soundstatus = true;
            menuSoundButton.transform.GetChild(0).gameObject.SetActive(true);
            menuSoundButton.transform.GetChild(1).gameObject.SetActive(false);
            settingsSoundButton.transform.GetChild(0).gameObject.SetActive(true);
            settingsSoundButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void MusicButton()
    {
        if (musicstatus)
        {
            GetComponent<AudioSource>().Stop();
            musicstatus = false;
            menuMusicButton.transform.GetChild(0).gameObject.SetActive(false);
            menuMusicButton.transform.GetChild(1).gameObject.SetActive(true);
            settingsMusicButton.transform.GetChild(0).gameObject.SetActive(false);
            settingsMusicButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            GetComponent<AudioSource>().Play();
            musicstatus = true;
            menuMusicButton.transform.GetChild(0).gameObject.SetActive(true);
            menuMusicButton.transform.GetChild(1).gameObject.SetActive(false);
            settingsMusicButton.transform.GetChild(0).gameObject.SetActive(true);
            settingsMusicButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}
