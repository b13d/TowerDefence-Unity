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
        if (_isLiveStage) { return; }

        _textsView.SetActive(true);
    }

    private void OnMouseExit()
    {
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
