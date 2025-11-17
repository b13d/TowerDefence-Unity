using System;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private int musicIndex;
    
    public static Sound instance;
    
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

    public void TurnSound()
    {
        _isAudio = !_isAudio;
    }

    public void TurnMusic()
    {
        _isMusic = !_isMusic;        
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