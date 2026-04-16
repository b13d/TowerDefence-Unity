using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject prefabTower;

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(prefabTower, Vector3.zero, Quaternion.identity);
    }
}
