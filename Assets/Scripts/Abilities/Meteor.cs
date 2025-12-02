using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] Texture2D textureMeteor;
    [SerializeField] List<Enemy> enemiesToDestroy;
    private bool _isUsed;
    Collider2D _collider;
    Abilities _abilities;

    public Abilities Abilities
    {
        set { _abilities = value; }
    }
    
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _abilities.MeteorUsed();
            _isUsed = true;
            _collider.enabled = true;
            StartCoroutine(DestroyMeteorAndEnemy());
        }

        if (!_isUsed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            transform.position = mousePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemiesToDestroy.Add(other.gameObject.GetComponent<Enemy>());
        }

        Debug.Log("Объект в зоне метеорита - " + other.name);
    }

    IEnumerator DestroyMeteorAndEnemy()
    {
        yield return new WaitForSeconds(0.15f);

        Destroy(gameObject);
    }
}