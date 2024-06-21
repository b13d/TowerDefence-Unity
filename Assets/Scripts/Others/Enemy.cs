using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<Vector2> path;
    public int indexPath;
    public GameObject lastTarget;
    public Vector2 target;

    public Slider sliderHealth;
    public float health;
    public float speed;
    public int levelEnemy;
    public bool isAFKEnemy;
    public bool isLiveStage;

    [SerializeField] SpriteRenderer _sprite;


    private void Start()
    {
        if (isAFKEnemy)
        {
            return;
        }

        StartCoroutine(WaitLoadedScript());

        target = path[indexPath];

        RotateEnemy();

        speed += 1.5f;
    }

    IEnumerator WaitLoadedScript()
    {
        yield return new WaitForSeconds(.1f);

        if (LevelLogic.instance == null && !isLiveStage)
        {
            StartCoroutine(WaitLoadedScript());
        } 
        else
        {
            if (!isLiveStage)
            {
                levelEnemy = LevelLogic.instance.GetCurrentLevel;
                health = LevelLogic.instance.currentWave * levelEnemy;
            } else
            {
                levelEnemy = 1;
                health = 2;
            }
                sliderHealth.maxValue = health;
            sliderHealth.value = health;
        }
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
            if (collision.GetComponent<Projectile>().targetEnemy == gameObject)
            {
                health -= collision.GetComponent<Projectile>().Damage;
                sliderHealth.value = health;

                GameObject traceHit = collision.GetComponent<Projectile>().GetTraceHit;

                if (traceHit != null)
                {
                    Instantiate(traceHit, transform.position, Quaternion.identity);
                }

                Destroy(collision.gameObject);

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
        }
    }
}
