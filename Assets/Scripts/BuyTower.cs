using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyTower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _price = 0;
    [SerializeField] int index;

    [SerializeField] GameObject _parent;

    [SerializeField] GameObject _place;

    [SerializeField] TextMeshProUGUI _txtPrice;

    private void Start()
    {
        _txtPrice.text = $"{_price}$";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.Money >= _price)
        {
            GameManager.instance.Money -= _price;

            LevelLogic.instance.UpdateTextMoney();

            _price = Mathf.FloorToInt(_price * 1.5f);

            Debug.Log($"newPrice: {_price}");

            _txtPrice.text = $"{_price}$";

            _parent.GetComponent<PlaceTower>().SelectedTower();

            Instantiate(_parent.GetComponent<PlaceTower>()._towers[index],
                _place.transform.position,
                Quaternion.identity, _place.transform);
        } 
        else
        {
            Debug.LogError("Не хватает денег!!");
        }
    }
}
