using System;
using UnityEngine;

public class Kernel : MonoBehaviour
{
    public GameObject target;
    public bool isFollowing;
    private float speed = 0.1f;
    int _damage;

    public int Damage
    {
        get { return _damage; }
        set
        {
            if (value > 0)
            {
                _damage = value;
            }
        }
    }

    void FixedUpdate()
    {
        speed += 0.1f;
        
        if (target)
        {
            isFollowing = true;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.5f * speed);
        } else if (isFollowing && !target)
        {
            Debug.Log("Снаряд очищается...");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Debug.LogError("Снаряд ударился об землю, удаляем");
            Destroy(gameObject);
        }
    }
}
