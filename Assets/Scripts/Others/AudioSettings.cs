using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private int musicIndex;
    
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
        mixer.GetFloat("Music_Volume", out float volume);
        mixer.SetFloat("Music_Volume", volume < 0 ? 0 : -80f);
    }


    public void InitialMusic()
    {
        musicSource.clip = musicClips[musicIndex];
        musicSource.Play();
    }
    
    public void NextMusic()
    {
    }
}