using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button eng, rus, soundOn, soundOff;
    public static bool sound;
    public static int lang;
    public static void Set(bool sounds, int langs)
    {
        sound = sounds;
        lang = langs;
    }
    public void SetSound(bool s)
    {
        sound = s;
    }
    public void Load()
    {
        if(sound)
        {
            soundOff.gameObject.SetActive(true);
            soundOn.gameObject.SetActive(false);
        }
        else
        {
            soundOff.gameObject.SetActive(false);
            soundOn.gameObject.SetActive(true);
        }
        switch(lang)
        {
            case 0:
                eng.interactable = false;
                rus.interactable = true;
                break;
            case 1:
                eng.interactable = true;
                rus.interactable = false;
                break;


        }
    }
}
