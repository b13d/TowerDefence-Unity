using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSpeedAttack : Skill
{
    public override void Start()
    {
        base.Start();
        
        if (tower.speedAttack <= 0.21f)
        {
            Destroy(gameObject);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        AddSpeedAttack();
        base.OnPointerClick(eventData);
    }


    void AddSpeedAttack()
    {
        if (LevelLogic.instance.playerValues.money >= price)
        {
            if (tower.speedAttack > 0.21f)
            {
                tower.ChangeSpeedAttack(.1f);
                LevelLogic.instance.playerValues.money -= price;
                price = Mathf.FloorToInt(price * INCREASE * tower.GetMarkup);

                SuccessBuy();

                if (tower.speedAttack <= 0.21f)
                {
                    audioSource.clip = lastUpdateSound;
                    audioSource.Play();
                    
                    sprite.DOColor(new Color(1, 1, 1, 0), 1f);
                    Destroy(gameObject, 1f);
                }
            }
        }
        else
        {
            ErrorBuy();
        }
    }
}