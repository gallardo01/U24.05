using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;

public class SoundManager : Singleton<SoundManager>, IGameStateListener
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void OnInit()
    {
        foreach (AudioClip clip in musicClips)
        {
            musicDict[clip.name] = clip;
        }

        foreach (AudioClip clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }
    }

    public void PlayMusic(string musicName)
    {
        if (musicDict.TryGetValue(musicName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip with name {musicName} not found!");
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX clip with name {sfxName} not found!");
        }
    }

    public void StopMusic() => musicSource.Stop();

    public void StopSFX() => sfxSource.Stop();

    public void SetMusicVolume(float volume) => musicSource.volume = volume;

    public void SetSFXVolume(float volume) => sfxSource.volume = volume;

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                if (musicDict.Count == 0) OnInit();
                PlayMusic("Menu_Music");
                break;

            case GameState.GAME:
                PlayMusic("Game_Music");
                break;            

            case GameState.GAMEOVER:
                StopMusic();
                break;

            case GameState.SHOP:
                PlayMusic("Shop_Music");
                break;
        }
    }
}
