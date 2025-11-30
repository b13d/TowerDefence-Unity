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
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return; 
    
        Debug.Log("=== CLICK DEBUG ===");
        Debug.Log($"PlaceTower: {gameObject.name}");
        Debug.Log($"Place reference: {_place.name}");
        Debug.Log($"Place instance ID: {_place.GetInstanceID()}");
        Debug.Log($"ChildCount: {_place.transform.childCount}");
    
        for(int i = 0; i < _place.transform.childCount; i++)
        {
            Transform child = _place.transform.GetChild(i);
            Debug.Log($"  Child {i}: {child.name}, active: {child.gameObject.activeSelf}");
        }
        Debug.Log("==================");

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
