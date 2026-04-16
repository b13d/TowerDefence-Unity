using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDisabled : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
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
        if (Input.GetMouseButtonDown(0) && isFloor)
        {
            isClicked = true;
        }
        
        if (Camera.main)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            isFloor = false;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 worldPoint = hit.point;
                worldPoint.y = 0;
                transform.position = worldPoint;

                if (hit.collider.CompareTag("PlaceTower"))
                {
                    isFloor = true;
                    
                    if (isClicked)
                    {
                        CubeTrigger cube = hit.collider.gameObject.GetComponent<CubeTrigger>();

                        if (!cube.hasTower)
                        {
                            cube.SetTower(towerPrefab);
                            Destroy(gameObject);
                        }
                    }
                }
            }
            
        }
    }
}