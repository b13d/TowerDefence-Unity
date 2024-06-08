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
    public int health;
    public float speed;
    public int levelEnemy;


    private void Start()
    {
        target = path[indexPath];
        speed += GameManager.instance.spawnerEnemy.currentWave * .5f;

        if (((GameManager.instance.spawnerEnemy.currentWave + 1) % 3) == 0)
        {
            health = (GameManager.instance.spawnerEnemy.currentWave + 1) * 2;
            sliderHealth.maxValue = health;
            sliderHealth.value = health;
        }
    }

    void FixedUpdate()
    {
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

        Debug.Log($"speed {speed}");

        var step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            GameManager.instance.UpdateHealth();
            GameManager.instance.EnemyFinished();

            Destroy(gameObject);
        }

        if (collision.tag == "Projectile")
        {
            if (collision.GetComponent<Projectile>().targetEnemy == gameObject)
            {
                health -= collision.GetComponent<Projectile>().Damage;
                sliderHealth.value = health;

                if (health <= 0)
                {
                    GameManager.instance.UpdateMoney(levelEnemy);
                    GameManager.instance.EnemyKill();

                    Destroy(gameObject);
                }
            }
        }
    }
}
