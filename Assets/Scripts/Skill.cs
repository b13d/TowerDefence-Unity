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


    public SkillType skillType;

    private void Start()
    {
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



    public void OnPointerClick(PointerEventData eventData)
    {

        if (skillType == SkillType.Damage)
        {
            Debug.Log("Damage");

            Debug.Log($"price: {_price}");

            if (GameManager.instance.Money >= _price)
            {
                _tower.ChangeDamageTower(.5f);

                GameManager.instance.Money -= _price;

                _price = Mathf.FloorToInt(_price * INCREASE * _tower.GetMarkup);
            }
            else
            {
                Debug.LogError("Не хватает денег");
            }

        } 
        else if (skillType == SkillType.SpeedAttack)
        {
            Debug.Log("SpeedAttack");

            Debug.Log($"price: {_price}");

            if (GameManager.instance.Money >= _price)
            {
                _tower.ChangeSpeedAttack(.1f);

                GameManager.instance.Money -= _price;

                _price = Mathf.FloorToInt(_price * INCREASE * _tower.GetMarkup);
            }
            else
            {
                Debug.LogError("Не хватает денег");
            }
            
        }
        else if (skillType == SkillType.Radius)
        {
            Debug.Log("Radius");

            Debug.Log($"price: {_price}");

            if (GameManager.instance.Money >= _price)
            {
                GameManager.instance.Money -= _price;

                _price = Mathf.FloorToInt(_price * INCREASE * _tower.GetMarkup);
            }
            else
            {
                Debug.LogError("Не хватает денег");
            }
        }

        _txtPriceSkill.text = _price.ToString();
        LevelLogic.instance.UpdateTextMoney();
    }
}
