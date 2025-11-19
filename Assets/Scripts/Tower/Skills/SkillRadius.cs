using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillRadius : Skill
{
    [SerializeField] RadiusTower _radiusTower;

    public override void Start()
    {
        base.Start();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        AddRadiusTower();
    }

    void AddRadiusTower()
    {
        if (LevelLogic.instance.playerValues.money >= price)
        {
            if (_radiusTower.xradius < 1.5f)
            {
                LevelLogic.instance.playerValues.money -= price;

                price = Mathf.FloorToInt(price * INCREASE * tower.GetMarkup);

                _radiusTower.ChangeScaleRadius();

                tower.towerMenu.UpdateRadiusText(_radiusTower.xradius);

                SuccessBuy();
            }
            else
            {
                sprite.DOColor(new Color(1, 1, 1, 0), 1f);
                Destroy(gameObject, 1f);
            }
        }
        else
        {
            ErrorBuy();
        }
    }
}
