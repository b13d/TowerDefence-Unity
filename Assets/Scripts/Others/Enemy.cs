using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Status Enemy")]
    public List<Vector2> path;
    public int indexPath;
    public GameObject lastTarget;
    public Vector2 target;
    public Slider sliderHealth;
    [SerializeField] SpriteRenderer _sprite;

    [Header("Parameters Enemy")]
    public float health;
    public float speed;
    public int levelEnemy;
    public bool isAFKEnemy;
    public bool isLiveStage;


    #region Methods
    private void Start()
    {
        if (isAFKEnemy)
        {
            return;
        }

        InitialValues();
    }

    void InitialValues()
    {
        if (!isLiveStage)
        {
            levelEnemy = LevelLogic.instance.GetCurrentLevel;
            health = LevelLogic.instance.currentWave * levelEnemy;
        }
        else
        {
            levelEnemy = 1;
            health = 2;
        }

        sliderHealth.maxValue = health;
        sliderHealth.value = health;

        target = path[indexPath];

        RotateEnemy();

        speed += 1.5f;
    }

    void FixedUpdate()
    {
        if (isAFKEnemy)
        {
            return;
        }

        if ((Vector2)transform.position == path[indexPath])
        {
            if (indexPath + 1 < path.Count)
            {
                indexPath++;

                target = path[indexPath];

                RotateEnemy();
            }
            else
            {
                target = lastTarget.transform.position;

                RotateEnemy();
            }
        }

        var step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    void RotateEnemy()
    {
        if (target.x < transform.position.x)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }
    }

    void Damage(Projectile projectile)
    {
        health -= projectile.Damage;
        sliderHealth.value = health;
    }

    void IsDead()
    {
        if (health <= 0)
        {
            if (!isLiveStage)
            {
                LevelLogic.instance.ProfitMoney(levelEnemy);
                LevelLogic.instance.EnemyKill();
            }

            Destroy(gameObject);
        }
    }

    void DestroyProjectile(Projectile projectile, Collider2D collision)
    {
        GameObject traceHit = projectile.GetTraceHit;

        if (traceHit != null)
        {
            Instantiate(traceHit, transform.position, Quaternion.identity);
        }

        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            if (!isLiveStage)
            {
                LevelLogic.instance.HitCastleHealth();
                LevelLogic.instance.allCounterEnemyDie++;
            }

            Destroy(gameObject);
        }

        if (collision.tag == "Projectile")
        {
            Projectile projectile = collision.GetComponent<Projectile>();

            if (projectile.targetEnemy == gameObject)
            {
                Damage(projectile);

                DestroyProjectile(projectile, collision);

                IsDead();
            }
        }
    }
    #endregion

}
