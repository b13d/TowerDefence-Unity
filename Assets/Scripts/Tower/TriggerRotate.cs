using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotate : MonoBehaviour
{
    public Vector3 rotation;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlaceTower"))
        {
            other.transform.rotation = Quaternion.Euler(rotation);
            Debug.Log("Коснулся PlaceTower!!");
        }
    }
}
