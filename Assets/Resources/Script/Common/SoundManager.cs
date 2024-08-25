using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> 
{
    public AudioSource[] BackgroundMusic;
    public AudioSource[] OneShotMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayBackgroundMusic(SoundList sound)
    {
        if (sound == SoundList.BackgroundMainMenu) 
        {
            BackgroundMusic[1].Stop();
            BackgroundMusic[0].Play();
        }
        else if (sound == SoundList.BackgroundInGame)
        {
            BackgroundMusic[0].Stop();
            BackgroundMusic[1].Play();  
        }
    }

    public void PlayOneShotMusic(SoundList sound)
    {
        if (sound == SoundList.ButtonClick)
        {
            OneShotMusic[0].Play();
        }
        else if (sound == SoundList.Shot)
        {
            OneShotMusic[1].Play();
        }
        else if (sound == SoundList.Dead)
        {
            OneShotMusic[2].Play();
        }
        else if (sound == SoundList.Win)
        {
            OneShotMusic[3].Play();
        }
    }
}

public enum SoundList
{
    BackgroundMainMenu,
    BackgroundInGame,
    ButtonClick,
    Shot,
    Dead,
    Win
}
