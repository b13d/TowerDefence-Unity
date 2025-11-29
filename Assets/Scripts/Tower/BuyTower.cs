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

    [SerializeField]
    bool _isTutorial;

    GameObject[] tutorialObject;

    private void Start()
    {
        if (_isTutorial)
        {
            tutorialObject = GameObject.FindGameObjectsWithTag("TutorialObject");
        }

        _txtPrice.text = $"{_price}$";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale == 0f) return;

        if (_isTutorial)
        {
            Buy();


            foreach(var t in tutorialObject)
            {
                Destroy(t.gameObject);    
            }

            return;
        }


        if (LevelLogic.instance.playerValues.money >= _price)
        {
            Buy();
        } 
        else
        {
        }
    }

    void Buy()
    {

        if (!_isTutorial)
        {
            LevelLogic.instance.playerValues.money -= _price;

            LevelLogic.instance.UpdateTextMoney();

            _price = Mathf.FloorToInt(_price * 1.5f);

            _txtPrice.text = $"{_price}$";
        }

        _parent.SelectedTower();

        Instantiate(_parent._towers[index],
            _place.transform.position,
            Quaternion.identity, _place.transform);
    }
}
