using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject prefabTower;
    [SerializeField] private CanvasSidebar canvasSidebar;
    [SerializeField] private int priceTower;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.money < priceTower)
        {
            Debug.Log("Недостаточно денег на покупку башни");
            return;
        }

        GameManager.Instance.BuyTower(priceTower);
        var tower = Instantiate(prefabTower, Vector3.zero, Quaternion.identity);
        canvasSidebar.SelectTower(tower);
    }
}
