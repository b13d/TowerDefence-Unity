using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private Sprite flagEn;
    [SerializeField] private Sprite flagRu;
    [SerializeField] private Button btnFlag;

    private void Start()
    {
        string language = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (language == "en")
        {
            btnFlag.image.sprite = flagEn;
        }
        else
        {
            btnFlag.image.sprite = flagRu;
        }
    }

    public void ChangeLanguage()
    {
        string language = LocalizationSettings.SelectedLocale.Identifier.Code;
        
        if (language == "en")
        {
            language = "ru";
            SwitchToRussian();
        }
        else
        {
            language = "en";
            SwitchToEnglish();
        }
        
        Debug.Log("language: " + language);
    }
    
    void SwitchToRussian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ru");
        btnFlag.image.sprite = flagRu;
    }

    void SwitchToEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
        btnFlag.image.sprite = flagEn;
    }
}
