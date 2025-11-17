using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject _canvasSettings;

    [Header("Sounds")] [SerializeField] Slider _sliderAudio;

    [SerializeField] Slider _sliderMusic;

    #region Methods

    private void Start()
    {
        _canvasSettings.SetActive(false);

        _sliderAudio.value = 0.5f;
        _sliderMusic.value = 0.25f;
    }

    public void ChangeValueMusic()
    {
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