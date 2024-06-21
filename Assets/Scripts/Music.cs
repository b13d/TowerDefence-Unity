using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;

    [SerializeField]
    List<AudioClip> _listMusic = new List<AudioClip>();

    AudioSource _audioSource;
    int _currentIndexMusic;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        UpdateValueMusic();

        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpdateValueMusic()
    {
        _audioSource.volume = Settings.instance.GetMusicVolume;
    }


    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            if (_currentIndexMusic < _listMusic.Count)
            {
                _currentIndexMusic = Random.Range(0, _listMusic.Count);

                _audioSource.clip = _listMusic[_currentIndexMusic];
                
                _currentIndexMusic++;
            }    
            else
            {
                _currentIndexMusic = 0;

                _audioSource.clip = _listMusic[_currentIndexMusic];
            }

            _audioSource.Play();
        }
    }
}
