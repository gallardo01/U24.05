using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;

public class SoundManager : Singleton<SoundManager>, IGameStateListener
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip[] musicClips;
    private AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void OnInit()
    {
        sfxClips = Resources.LoadAll<AudioClip>("SFX/Sound");

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

    public static void StopSFX() => Instance.sfxSource.Stop();

    public void SetMusicVolume(float volume) => musicSource.volume = volume;

    public void SetSFXVolume(float volume) => sfxSource.volume = volume;


    public static void ButtonClick() => Instance.PlaySFX("Click " + "(" + Random.Range(1, 11) + ")");
    public static void CurrencyClick() => Instance.PlaySFX("Collect " + "(" + Random.Range(1, 7) + ")");
    public static void Throw() => Instance.PlaySFX("Throw " + "(" + Random.Range(1, 16) + ")");
    public static void PLayerTalk() => Instance.PlaySFX("Phase " + "(" + Random.Range(1, 56) + ")");
    public static void CharacterDead() => Instance.PlaySFX("Dead " + "(" + Random.Range(1, 25) + ")");
    public static void LevelUp() => Instance.PlaySFX("LevelUp " + "(" + Random.Range(1, 12) + ")");

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
                PlaySFX("Win " + "(" + Random.Range(1, 6) + ")"); ;
                break;

            case GameState.SHOP:
                PlayMusic("Shop_Music");
                break;
        }
    }
}
