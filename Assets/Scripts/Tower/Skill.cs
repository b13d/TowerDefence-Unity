using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SkillType
{
    Damage,
    SpeedAttack,
    Radius
}


public class Skill : MonoBehaviour, IPointerClickHandler
{
    const float INCREASE = 2f;

    [SerializeField] GameObject _hint;
    [SerializeField] Tower _tower;
    [SerializeField] int _price;
    [SerializeField] TextMeshProUGUI _txtPriceSkill;
    [SerializeField] RadiusTower _radiusTower;
    [SerializeField] SpriteRenderer _sprite;

    [Header("Sounds")]
    [SerializeField] AudioClip _buyingSuccess;
    [SerializeField] AudioClip _buyingError;

    private AudioSource _audioSource;

    public SkillType skillType;

    #region Methods
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _hint.SetActive(false);
        _txtPriceSkill.text = _price.ToString();
    }

    private void OnMouseEnter()
    {
        _hint.SetActive(true);
    }

    private void OnMouseExit()
    {
        _hint.SetActive(false);
    }


    void SuccessBuy()
    {
        // _audioSource.volume = Settings.instance.GetAudioVolume;

        _audioSource.clip = _buyingSuccess;

        _audioSource.Play();
    }

    void ErrorBuy()
    {
        // _audioSource.volume = Settings.instance.GetAudioVolume;

        _audioSource.clip = _buyingError;

        _audioSource.Play();
    }

    void AddDamage()
    {
        if (LevelLogic.instance.playerValues.money >= _price)
        {
            _tower.ChangeDamageTower(.5f);

            LevelLogic.instance.playerValues.money -= _price;

            _price = Mathf.FloorToInt(_price * INCREASE * _tower.GetMarkup);

            SuccessBuy();
        }
        else
        {
            Debug.LogError("�� ������� �����");

            ErrorBuy();
        }
    }

    void AddSpeedAttack()
    {
        if (LevelLogic.instance.playerValues.money >= _price)
        {
            if (_tower.speedAttack > 0.21f)
            {
                _tower.ChangeSpeedAttack(.1f);

                LevelLogic.instance.playerValues.money -= _price;

                _price = Mathf.FloorToInt(_price * INCREASE * _tower.GetMarkup);

                SuccessBuy();
            }
            else
            {
                Debug.LogError("��������� ����� ����� ��������");

                _sprite.DOColor(new Color(1, 1, 1, 0), 1f);
                Destroy(gameObject, 1f);
            }
        }
        else
        {
            Debug.LogError("�� ������� �����");

            ErrorBuy();
        }
    }

    void AddRadiusTower()
    {
        if (LevelLogic.instance.playerValues.money >= _price)
        {
            if (_radiusTower.xradius < 1.5f)
            {
                LevelLogic.instance.playerValues.money -= _price;

                _price = Mathf.FloorToInt(_price * INCREASE * _tower.GetMarkup);

                _radiusTower.ChangeScaleRadius();

                _tower.towerMenu.UpdateRadiusText(_radiusTower.xradius);

                SuccessBuy();
            }
            else
            {
                Debug.LogError("��������� ����� �������");

                _sprite.DOColor(new Color(1, 1, 1, 0), 1f);
                Destroy(gameObject, 1f);
            }
        }
        else
        {
            Debug.LogError("�� ������� �����");

            ErrorBuy();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (skillType == SkillType.Damage)
        {
            AddDamage();
        }
        else if (skillType == SkillType.SpeedAttack)
        {
            AddSpeedAttack();
        }
        else if (skillType == SkillType.Radius)
        {
            AddRadiusTower();
        }

        _txtPriceSkill.text = _price.ToString();
        LevelLogic.instance.UpdateTextMoney();
    }
    #endregion










}
