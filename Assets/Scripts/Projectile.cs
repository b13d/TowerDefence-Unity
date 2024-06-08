using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject targetEnemy;

    public bool isRotate;

    [SerializeField]
    int damage = 0;

    Rigidbody2D _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    public int Damage
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
                Vector3 diference = targetEnemy.transform.position - transform.position;
                float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + 90);

                // ������ ������� � ������� ����������
            }

        } else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}