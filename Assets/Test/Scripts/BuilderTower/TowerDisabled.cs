using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDisabled : MonoBehaviour
{
    public int z;
    public bool isClicked;
    public bool isFloor;

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
        Debug.Log("Позиция башни: " + transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            if (isFloor)
            {
                isClicked = true;
            }
        }

        if (Camera.main && !isClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            isFloor = false;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {

                if (hit.collider.CompareTag("PlaceTower"))
                {
                    isFloor = true;
                }
                
                Vector3 worldPoint = hit.point;
                worldPoint.y = 0;
                transform.position = worldPoint;
                Debug.Log("did hit worldPoint: " + worldPoint);
            }
            
        }
    }
}