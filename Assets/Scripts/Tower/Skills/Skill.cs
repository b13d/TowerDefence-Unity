using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// public enum SkillType
// {
//     Damage,
//     SpeedAttack,
//     Radius
// }


public class Skill : MonoBehaviour, IPointerClickHandler
{
    public const float INCREASE = 2f;
    public Tower tower;
    public int price;
    public SpriteRenderer sprite;

    [SerializeField] TextMeshProUGUI _txtPriceSkill;
    [SerializeField] GameObject hint;

    [Header("Sounds")]
    [SerializeField] AudioClip _buyingSuccess;
    [SerializeField] AudioClip _buyingError;
    public AudioClip lastUpdateSound;

    public AudioSource audioSource;

    // public SkillType skillType;

    #region Methods
    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hint.SetActive(false);
        _txtPriceSkill.text = price.ToString();
    }

    private void OnMouseEnter()
    {
        hint.SetActive(true);
    }

    private void OnMouseExit()
    {
        hint.SetActive(false);
    }


    public void SuccessBuy()
    {
        audioSource.clip = _buyingSuccess;
        audioSource.Play();
    }

    public void ErrorBuy()
    {
        audioSource.clip = _buyingError;
        audioSource.Play();
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        _txtPriceSkill.text = price.ToString();
        LevelLogic.instance.UpdateTextMoney();
    }
    #endregion










}
