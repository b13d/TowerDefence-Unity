using System;
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

        StartCoroutine(PlaySoundAndWait());
    }

    IEnumerator PlaySoundAndWait()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        
        yield return new WaitWhile(() => _audioSource.isPlaying);

        Destroy(gameObject);
    }
}
