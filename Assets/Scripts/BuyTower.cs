using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyTower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _price = 0;
    [SerializeField] int index;

    [SerializeField] GameObject _parent;

    [SerializeField] GameObject _place;

    public void OnPointerClick(PointerEventData eventData)
    {
        _parent.GetComponent<PlaceTower>().SelectedTower();

        Instantiate(_parent.GetComponent<PlaceTower>()._towers[index],
            _place.transform.position,
            Quaternion.identity,_place.transform);

        Debug.Log($"Price: {_price}");
    }
}
