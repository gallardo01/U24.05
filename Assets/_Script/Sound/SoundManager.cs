using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource[] audioBackground;
    public AudioSource[] audioOneShot;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBackgroundMusic(SoundList sound)
    {
        if (sound == SoundList.BackgroundMainMenu)
        {
            audioBackground[0].Play();
            audioBackground[1].Stop();
            audioBackground[2].Stop();
        }
        if (sound == SoundList.BackgroundIngame)
        {
            audioBackground[0].Stop();
            audioBackground[1].Stop();
            audioBackground[2].Play();
        }
        if (sound == SoundList.Shop)
        {
            audioBackground[0].Stop();
            audioBackground[1].Play();
            audioBackground[2].Stop();
        }
    }
    public void PlayOneShot(SoundList sound)
    {
        if (sound == SoundList.ButtonClick)
        {
            audioOneShot[0].Play();
        }
        else if (sound == SoundList.Shot)
        {
            audioOneShot[1].Play();
        }
        else if (sound == SoundList.Win)
        {
            audioOneShot[2].Play();
        }
        else if (sound == SoundList.Sizeup)
        {
            audioOneShot[3].Play();
        }
    }

    public enum SoundList
    {
        BackgroundMainMenu,
        BackgroundIngame,
        Shop,
        ButtonClick,
        Shot,
        Win,
        Sizeup
    }
}
