using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private int musicIndex;

    private int lastIndexMusic = -1;
    public static AudioSettings instance;

    public AudioMixer mixer;

    
    private bool _isAudio = true;
    private bool _isMusic = true;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitialMusic();
        DontDestroyOnLoad(gameObject);
    }

    public void SetSFXEnabled()
    {
        mixer.GetFloat("SFX_Volume", out float volume);
        mixer.SetFloat("SFX_Volume", volume < 0 ? 0 : -80f);
    }
    
    public void SetMusicEnabled()
    {
        mixer.GetFloat("Win_Lose_Volume", out float volume_win_lose);
        mixer.SetFloat("Win_Lose_Volume", volume_win_lose < -79f ? -5f : -80f);
        mixer.GetFloat("Music_Volume", out float volume);
        mixer.SetFloat("Music_Volume", volume < -79f ? -10f : -80f);
    }

    public void Reset()
    {
        musicSource.mute = false;
    }
    
    public IEnumerator CongratulationMusic(float time)
    {        
        musicSource.mute = true;
        yield return new WaitForSecondsRealtime(time);
        musicSource.mute = false;
    }


    public void InitialMusic()
    {
        Debug.LogError("InitialMusic");

        while (musicIndex == lastIndexMusic)
        {
            Debug.LogError("Подбираю index music");
            musicIndex = Random.Range(0, musicClips.Length);
        }
        
        musicSource.clip = musicClips[musicIndex];
        musicSource.Play();
        StartCoroutine(PlayMusicAndWait());
    }

    IEnumerator PlayMusicAndWait()
    {
        yield return new WaitWhile(() => musicSource.isPlaying);
        lastIndexMusic = musicIndex;
        InitialMusic();
    }
    
    public void NextMusic()
    {
    }
}