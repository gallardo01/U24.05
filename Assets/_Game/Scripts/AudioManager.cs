using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private List<AudioSource> listAudioSource = new();

    private List<int> listAudioSourceReady = new();
    private List<int> listAudioSourceBusy = new();

    [SerializeField] GameObject audioSourceParent;

    [SerializeField] List<SFXAudioClip> listSFXAudioClip = new();
    private Dictionary<SFXType, AudioClip> dictSFXAudioClip = new();

    private void Awake()
    {
        InitDictSFX();
    }

    private void InitDictSFX()
    {
        for (int i = 0; i < listSFXAudioClip.Count; i++)
        {
            dictSFXAudioClip.Add(listSFXAudioClip[i].type, listSFXAudioClip[i].clip);
        }
    }

    public void PlaySFX(SFXType type)
    {
        int audioSourceIndex = GetAudioSourceReady();

        listAudioSource[audioSourceIndex].clip = GetClip(type);
        //listAudioSource[audioSourceIndex].loop = false;
        listAudioSource[audioSourceIndex].Play();

        StartCoroutine(ReturnAudioSourceReady(audioSourceIndex, listAudioSource[audioSourceIndex]));
    }

    private int GetAudioSourceReady()
    {
        if (listAudioSourceReady.Count == 0)
        {
            AudioSource newAudioSource = audioSourceParent.AddComponent<AudioSource>();
            listAudioSourceReady.Add(listAudioSource.Count);
            listAudioSource.Add(newAudioSource);
        }

        int audioSourceIndex = listAudioSourceReady[0];
        listAudioSourceBusy.Add(audioSourceIndex);
        listAudioSourceReady.RemoveAt(0);

        return audioSourceIndex;
    }

    private IEnumerator ReturnAudioSourceReady(int audioSourceIndex, AudioSource audioSource)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        listAudioSourceBusy.Remove(audioSourceIndex);
        listAudioSourceReady.Add(audioSourceIndex);
    }

    private AudioClip GetClip(SFXType type)
    {
        if (dictSFXAudioClip.TryGetValue(type, out AudioClip clip))
        {
            return clip;
        }
        else
        {
            return null;
        }
    }
}


[System.Serializable]
public class SFXAudioClip
{
    public SFXType type;
    public AudioClip clip;
}