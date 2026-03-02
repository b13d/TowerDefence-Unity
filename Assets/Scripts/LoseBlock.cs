using System;
using UnityEngine;

public class LoseBlock : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            GameObject parent = enemy.gameObject;
            GameManager.Instance.AddDamage(enemy.damage);
            GameManager.Instance.DeadEnemy();
            Destroy(parent);
        }
    }
}
