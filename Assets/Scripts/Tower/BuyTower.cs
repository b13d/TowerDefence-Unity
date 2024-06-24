using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyTower : MonoBehaviour, IPointerClickHandler
{

    [Header("Tower Values")]
    [SerializeField] private int _price = 0;
    [SerializeField] int index;

    [Header("Tower GameObjects")]
    [SerializeField] PlaceTower _parent;

    [SerializeField] GameObject _place;

    [SerializeField] TextMeshProUGUI _txtPrice;

    private void Start()
    {
        _txtPrice.text = $"{_price}$";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (LevelLogic.instance.playerValues.money >= _price)
        {
            Buy();
        } 
        else
        {
            Debug.LogError("Не хватает денег!!");
        }
    }

    void Buy()
    {
        LevelLogic.instance.playerValues.money -= _price;

        LevelLogic.instance.UpdateTextMoney();

        _price = Mathf.FloorToInt(_price * 1.5f);

        _txtPrice.text = $"{_price}$";

        _parent.SelectedTower();

        Instantiate(_parent._towers[index],
            _place.transform.position,
            Quaternion.identity, _place.transform);
    }
}
