using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _projectile;

    public int damage = 1;

    float _seconds = .3f;

    
    void Update()
    {
        if (_target != null)
        {
            _seconds -= Time.deltaTime;

            if (_seconds < 0)
            {
                _seconds = .3f;
                Shoot();
            }

        }
    }

    void Shoot()
    {
        var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);

        Projectile projectilePrefab = projectile.GetComponent<Projectile>();

        projectilePrefab.targetEnemy = _target;
        projectilePrefab.Damage = damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _target)
        {
            _target = null;
        }
    }



}
