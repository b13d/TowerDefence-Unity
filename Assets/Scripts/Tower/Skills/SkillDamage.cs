using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDamage : Skill
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        AddDamage();
        base.OnPointerClick(eventData);
    }

    void AddDamage()
    {
        if (LevelLogic.instance.playerValues.money >= price)
        {
            tower.ChangeDamageTower(.5f);

            LevelLogic.instance.playerValues.money -= price;

            price = Mathf.FloorToInt(price * INCREASE * tower.GetMarkup);

            SuccessBuy();
        }
        else
        {
            ErrorBuy();
        }
    }
}
