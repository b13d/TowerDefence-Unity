using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] private Material mDisable;
    [SerializeField] private Material mFocus;
    
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            meshRenderer.material = mFocus;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            meshRenderer.material = mDisable;
        }
    }
}
