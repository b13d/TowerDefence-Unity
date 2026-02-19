using System;
using UnityEngine;

public class LoseBlock : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            GameManager.Instance.AddDamage(enemy.damage);
            GameManager.Instance.DeadEnemy();
            Destroy(other.gameObject);
        }
    }
}
