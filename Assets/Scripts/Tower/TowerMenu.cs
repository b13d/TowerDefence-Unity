using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerMenu : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private TextMeshProUGUI _txtDamage;

    [SerializeField]
    private TextMeshProUGUI _txtSpeedAttack;

    [SerializeField]
    private TextMeshProUGUI _txtRadiusTower;

    [SerializeField]
    private GameObject _textsView;

    [SerializeField]
    private GameObject _skillsView;

    [SerializeField]
    List<Skill> _skills = new List<Skill>();

    [SerializeField]
    float markup;

    [SerializeField]
    bool _isLiveStage;

    [SerializeField]
    LineRenderer _line;

    Color colorEmpty = new Color(1, 1, 1, 0);

    public float GetMarkup
    {
        get { return markup; }
    }

    private void Start()
    {
        _textsView.SetActive(false);
        _skillsView.SetActive(false);
    }

    public void UpdateTexts(float damage, float speedAttack)
    {
        _txtDamage.text = $"Damage: {damage}";
        _txtSpeedAttack.text = $"Attack speed: {speedAttack}";
    }

    public void UpdateRadiusText(float radius)
    {
        _txtRadiusTower.text = $"Radius Tower: {radius}";
    }

    private void OnMouseEnter()
    {
        _line.DOColor(new Color2(colorEmpty, colorEmpty), new Color2(Color.white, Color.white), .5f);

        if (_isLiveStage) { return; }

        _textsView.SetActive(true);
    }

    private void OnMouseExit()
    {
        _line.DOColor(new Color2(Color.white, Color.white), new Color2(colorEmpty, colorEmpty), .5f);

        if (_isLiveStage) { return; }

        _textsView.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isLiveStage) { return; }

        _skillsView.SetActive(!_skillsView.activeSelf);
    }


    public void DestoySelf()
    {
        if (_isLiveStage) { return; }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
