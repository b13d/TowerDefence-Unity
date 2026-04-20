using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject prefabTower;
    [SerializeField] private CanvasSidebar canvasSidebar;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var tower = Instantiate(prefabTower, Vector3.zero, Quaternion.identity);
        canvasSidebar.SelectTower(tower);
    }
}
