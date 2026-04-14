using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDisabled : MonoBehaviour
{
    public int z;
    
    void Update()
    {
        if (Camera.main)
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
