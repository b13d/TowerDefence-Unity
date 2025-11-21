using UnityEngine;
using UnityEngine.Localization;

namespace Ui
{
    public class LanguageManager : MonoBehaviour
    {
        [SerializeField] private LocalizedString[] englishTexts;
        [SerializeField] private LocalizedString[] russianTexts;

        [SerializeField] private TMPro.TextMeshProUGUI englishTextUI;
        [SerializeField] private TMPro.TextMeshProUGUI russianTextUI;
        
        void Start()
        {
            englishTexts[0].StringChanged += UpdateEnglishText;
            russianTexts[0].StringChanged += UpdateRussianText;
        }


        void UpdateEnglishText(string value)
        {
            englishTextUI.text = value;
        }

        void UpdateRussianText(string value)
        {
            russianTextUI.text = value;
        }
    }
}
