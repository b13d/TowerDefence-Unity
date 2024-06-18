using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyTower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _price = 0;
    [SerializeField] int index;

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
            LevelLogic.instance.playerValues.money -= _price;

            LevelLogic.instance.UpdateTextMoney();

            _price = Mathf.FloorToInt(_price * 1.5f);

            Debug.Log($"newPrice: {_price}");

            _txtPrice.text = $"{_price}$";

            _parent.SelectedTower();

            var newTower = Instantiate(_parent._towers[index],
                _place.transform.position,
                Quaternion.identity, _place.transform);

            //_parent.speedAttackTower = newTower.GetComponent<Tower>().speedAttack;
            //_parent.damageTower = newTower.GetComponent<Tower>().damage;

            //_parent.SetStats();

            Debug.Log($"newTower.GetComponent<Tower>().speedAttack {newTower.GetComponent<Tower>().speedAttack}");
            Debug.Log($"newTower.GetComponent<Tower>().damage {newTower.GetComponent<Tower>().damage}");
        } 
        else
        {
            Debug.LogError("Не хватает денег!!");
        }
    }
}
