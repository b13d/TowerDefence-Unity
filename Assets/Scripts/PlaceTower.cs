using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] _towers;
    


    bool _isSelectedTower;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isSelectedTower)
        {
            return;
        }


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


    public void SelectedTower()
    {
        _isSelectedTower = true;

        GetComponent<Animator>().SetBool("isClose", true);
        GetComponent<Animator>().SetBool("isOpen", false);
       
    }

}
