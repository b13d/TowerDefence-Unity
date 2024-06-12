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


    private void Start()
    {
        if (isAFKEnemy)
        {
            return;
        }

        levelEnemy = LevelLogic.instance.GetCurrentLevel;

        health = LevelLogic.instance.currentWave * levelEnemy;
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
        
        target = path[indexPath];
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
            } 
            else
            {
                target = lastTarget.transform.position;
            }
        }

        var step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            LevelLogic.instance.HitCastleHealth();
            LevelLogic.instance.EnemyKill();

            Destroy(gameObject);
        }

        if (collision.tag == "Projectile")
        {
            if (collision.GetComponent<Projectile>().targetEnemy == gameObject)
            {
                health -= collision.GetComponent<Projectile>().Damage;
                sliderHealth.value = health;

                Destroy(collision.gameObject);

                if (health <= 0)
                {
                    LevelLogic.instance.ProfitMoney(levelEnemy);
                    LevelLogic.instance.EnemyKill();
                    
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}
