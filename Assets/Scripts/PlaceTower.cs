using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] _towers;
    bool _isSelectedTower;

    [SerializeField] GameObject _buttonDeleteTower;
    [SerializeField] GameObject _place;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isSelectedTower)
        {
            _buttonDeleteTower.SetActive(!_buttonDeleteTower.activeSelf);
        } 
        else
        {
            if (GetComponent<Animator>().GetBool("isClose") ||
            (!GetComponent<Animator>().GetBool("isClose") &&
            !GetComponent<Animator>().GetBool("isOpen")))
            {
                GetComponent<Animator>().SetBool("isOpen", true);
                GetComponent<Animator>().SetBool("isClose", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("isClose", true);
                GetComponent<Animator>().SetBool("isOpen", false);
            }



            Debug.Log("Кликнул на основание");
        }


        
    }


    public void ClearTower()
    {
        _isSelectedTower = false;
        _buttonDeleteTower.SetActive(false);

        int i = 0;

        GameObject[] allChildrens = new GameObject[_place.transform.childCount];

        foreach (Transform child in _place.transform)
        {
            allChildrens[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildrens)
        {
            Destroy(child.gameObject);
        }
    }

    public void SelectedTower()
    {
        _isSelectedTower = true;

        GetComponent<Animator>().SetBool("isClose", true);
        GetComponent<Animator>().SetBool("isOpen", false);
       
    }

}
