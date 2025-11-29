using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class TowerMenu : MonoBehaviour, IPointerClickHandler
{
    [Header("TextMeshProGUI")]
    [SerializeField]
    private TextMeshProUGUI _txtDamage;
    [SerializeField]
    private TextMeshProUGUI _txtSpeedAttack;
    [SerializeField]
    private TextMeshProUGUI _txtRadiusTower;
    
    [Header("Objects")]
    [SerializeField]
    private GameObject _textsView;
    [SerializeField]
    private GameObject _skillsView;
    [SerializeField]
    List<Skill> _skills = new List<Skill>();
    [SerializeField]
    LineRenderer _line;



    [SerializeField]
    float markup;
    [SerializeField]
    bool _isLiveStage;

    Color colorEmpty = new Color(1, 1, 1, 0);

    public float GetMarkup => markup;

    #region Methods
    private void Start()
    {
        _textsView.SetActive(false);
        _skillsView.SetActive(false);
    }

    public void UpdateTexts(float damage, float speedAttack)
    {
        string language = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (language == "en")
        {
            _txtDamage.text = $"Damage: {damage}";
            _txtSpeedAttack.text = $"Attack speed: {speedAttack}";
        }
        else
        {
            _txtDamage.text = $"Урон: {damage}";
            _txtSpeedAttack.text = $"Скорость атаки: {speedAttack}";
        }
    }

    public void UpdateRadiusText(float radius)
    {
        string language = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (language == "en")
        {
            _txtRadiusTower.text = $"Radius: {radius}";
        }
        else
        {
            _txtRadiusTower.text = $"Радиус: {radius}";
        }
        
    }

    private void OnMouseEnter()
    {
        if (Time.timeScale == 0) return;
        
        _line.DOColor(new Color2(colorEmpty, colorEmpty), new Color2(Color.white, Color.white), .5f);

        if (_isLiveStage) { return; }

        _textsView.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (Time.timeScale == 0) return;

        _line.DOColor(new Color2(Color.white, Color.white), new Color2(colorEmpty, colorEmpty), .5f);

        if (_isLiveStage) { return; }

        _textsView.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return;

        if (_isLiveStage) { return; }

        _skillsView.SetActive(!_skillsView.activeSelf);
    }


    public void DestoySelf()
    {
        if (_isLiveStage) { return; }

        Destroy(gameObject.transform.parent.gameObject);
    }

    #endregion


}
