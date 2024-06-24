using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [SerializeField]
    GameObject _canvasSettings;

    [Header("Sounds")]
    [SerializeField]
    Slider _sliderAudio;

    [SerializeField]
    Slider _sliderMusic;

    #region Methods
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

    #endregion

    #region Properties
    public float GetAudioVolume => _sliderAudio.value;

    public float GetMusicVolume => _sliderMusic.value;

    #endregion
}
