using System;
using UnityEngine;

public class Kernel : MonoBehaviour
{
    public Enemy target;
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
            transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, 0.5f * speed);
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
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();

            if (enemy == target)
            {
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Ground"))
        {
            Debug.LogWarning("Снаряд ударился об землю, удаляем");
            Destroy(gameObject);
        }
    }
}
