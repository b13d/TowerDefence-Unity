using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDisabled : MonoBehaviour
{
    public int z;
    public bool isClicked;

    private void Start()
    {
        StartCoroutine(CursorF());
    }

    IEnumerator CursorF()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.01f);
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        
        if (Camera.main && !isClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 worldPoint = hit.point;
                transform.position = worldPoint;
                Debug.Log("did hit worldPoint: " + worldPoint);
            }
        }
    }
}
