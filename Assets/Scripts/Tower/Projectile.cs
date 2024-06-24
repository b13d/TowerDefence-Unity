using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject targetEnemy;
    public bool isRotate;
    [SerializeField]
    float damage = 0;
    [SerializeField]
    GameObject _traceHitEnemy;
    Rigidbody2D _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    public GameObject GetTraceHit => _traceHitEnemy;

    public float Damage
    {
        get { return damage; }
        set { if (value >= 1) damage = value; }
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, .9f);
        
            if (!isRotate)
            {
                RotateProjectile();
                // turn the object toward the enemy
            }

        } 
        else
        {
            Destroy(gameObject);
        }
    }

    void RotateProjectile()
    {
        Vector3 diference = targetEnemy.transform.position - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + 90);
    }

    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, 5);
        }
    }
}
