using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    AudioSource _audioSource;

    [SerializeField] bool _isSoundTowerShoot;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_isSoundTowerShoot)
        {
            if (Settings.instance.GetAudioVolume > .2f)
            {
                _audioSource.volume = .2f;
            } 
            else
            {
                _audioSource.volume = Settings.instance.GetAudioVolume;
            }
        } 
        else
        {
            _audioSource.volume = Settings.instance.GetAudioVolume;
        }
    }

}
