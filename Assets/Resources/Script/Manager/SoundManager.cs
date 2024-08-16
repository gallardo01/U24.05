using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sound Settings")]
    public AudioSource soundSource; // AudioSource for playing sound effects
    public AudioClip[] audioClips;  // Array of AudioClips for different sounds

    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();


    private void Awake()
    {
        InitSound();
    }

    private void InitSound()
    {
        foreach (AudioClip clip in audioClips)
        {
            soundDictionary[clip.name] = clip;
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            soundSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
        }
    }
}
