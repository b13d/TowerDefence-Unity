using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveForward : MonoBehaviour
{
    public int speed;
    public Vector3 startPosition;


    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnDisable()
    {
        transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
    }
}
