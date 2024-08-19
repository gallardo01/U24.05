using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public enum SoundType
    {
        BackgroundMainMenu,
        BackgroundGamePlay,
        Shot,
        Click,
        Dead,
        Win
    }
    public AudioSource[] BackgroundMusic;

    public AudioSource[] OneShotMusic;
   
    public void PlayBackGroundMusic(SoundType sound)
    {
        if (sound == SoundType.BackgroundMainMenu)
        {
            BackgroundMusic[0].Play();
            BackgroundMusic[1].Stop();
        }
        else if (sound == SoundType.BackgroundGamePlay)
        {
            BackgroundMusic[1].Play();
            BackgroundMusic[0].Stop();
        }
    }
    
    public void PlayOneShotMusic(SoundType sound)
    {
        if (sound == SoundType.Click)
        {
            OneShotMusic[0].Play();
        }
        else if (sound == SoundType.Shot)
        {
            OneShotMusic[1].Play();
        }
        else if (sound == SoundType.Dead)
        {
            OneShotMusic[2].Play();
        }
        else if (sound == SoundType.Win)
        {
            OneShotMusic[3].Play();
        }
    }
}
