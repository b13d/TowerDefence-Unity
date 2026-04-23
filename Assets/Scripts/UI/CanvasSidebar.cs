using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasSidebar : MonoBehaviour
{
    [SerializeField] private GameObject tooltip;
    [SerializeField] private TextMeshProUGUI textTooltip;

    [SerializeField] GameObject currentTower;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (currentTower != null)
            {
                Debug.Log("Удаление башни при постройке, на правое нажатие мыши");
                Destroy(currentTower);
            }
        }
    }

    public TextMeshProUGUI TextTooltip
    {
        get
        {
            tooltip.SetActive(true);
            return textTooltip;
        }
    }

    public void TooltipOff()
    {
        tooltip.SetActive(false);
        textTooltip.text = "";
    }

    private void Start()
    {
        tooltip.SetActive(false);
    }

    public void SelectTower(GameObject tower)
    {
        if (currentTower)
        {
            if (currentTower)
            {
                Destroy(currentTower);
            }
        }

        currentTower = tower;
    }
}