using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private List<AudioSource> audioSources = new List<AudioSource>();

    private List<int> audioSourcesReady = new List<int>();
    private List<int> audioSourcesUsed = new List<int>();

    [SerializeField] GameObject audioSourcesParent;

    [SerializeField] List<SFXAudioClip> clips = new List<SFXAudioClip>();


    public void PlaySFX(SFXType type)
    {
        int audioSourceIndex = GetAudioSourceReady();

        audioSources[audioSourceIndex].clip = GetClip(type);
        audioSources[audioSourceIndex].loop = false;
        audioSources[audioSourceIndex].Play();

        StartCoroutine(ReturnAudioSourceReady(audioSourceIndex, audioSources[audioSourceIndex]));
    }

    private int GetAudioSourceReady()
    {
        if (audioSourcesReady.Count == 0)
        {
            AudioSource newAudioSource = audioSourcesParent.AddComponent<AudioSource>();
            audioSourcesReady.Add(audioSources.Count);
            audioSources.Add(newAudioSource);
        }

        int audioSourceIndex = audioSourcesReady[0];
        audioSourcesUsed.Add(audioSourceIndex);
        audioSourcesReady.RemoveAt(0);

        return audioSourceIndex;
    }

    private IEnumerator ReturnAudioSourceReady(int audioSourceIndex, AudioSource audioSource)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioSourcesUsed.Remove(audioSourceIndex);
        audioSourcesReady.Add(audioSourceIndex);
    }

    private AudioClip GetClip(SFXType type)
    {
        for (int i = 0; i < clips.Count; i++)
        {
            if (clips[i].type == type)
            {
                return clips[i].clip;
            }
        }

        return null;
    }
}

[System.Serializable]
public class SFXAudioClip
{
    public SFXType type;
    public AudioClip clip;
}