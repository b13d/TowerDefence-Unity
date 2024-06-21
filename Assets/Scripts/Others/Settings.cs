using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [SerializeField]
    GameObject _canvasSettings;

    [SerializeField]
    Slider _sliderAudio;

    [SerializeField]
    Slider _sliderMusic;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            _canvasSettings.SetActive(false);
            
            _sliderAudio.value = .5f;
            _sliderMusic.value = .25f;

            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeValueMusic()
    {
        if (Music.instance)
        {
            Music.instance.UpdateValueMusic();
        }
    }

    public void ShowSettings()
    {
        _canvasSettings.SetActive(true);
    }

    public float GetAudioVolume
    {
        get { return _sliderAudio.value; }
    }

    public float GetMusicVolume
    {
        get { return _sliderMusic.value; }
    }
}
