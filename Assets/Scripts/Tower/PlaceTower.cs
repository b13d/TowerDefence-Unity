using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] _towers;
    [SerializeField]
    GameObject _place;
    [SerializeField]
    bool _isLiveStage;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click on place tower");

        if (_isLiveStage)
        {
            return;
        }

        if(_place.transform.childCount == 0)
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
        }
    }

    public void SelectedTower()
    {
        GetComponent<Animator>().SetBool("isClose", true);
        GetComponent<Animator>().SetBool("isOpen", false);
    }
}
