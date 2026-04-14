using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerClickHandler
{
    private bool isFocused = false;

    private void Update()
    {
        if (isFocused)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isFocused = !isFocused;
    }
}
