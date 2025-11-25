using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject[] prefabsEnemy;
    public int spawnAmount;
    public int countEnemy;
    public GameObject[] pointsSpawner;
    public GameObject target;
    public List<Enemy> enemies;
    public List<GameObject> wayPoints;

    private void OnEnable()
    {
        Enemy.OnEnemyDied += EnemyDie;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= EnemyDie;
    }

    void Start()
    {
        // Debug.Log("Количество точке пути: " + paths[0].transform.getchil);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1f);

        if (spawnAmount > 0)
        {
            List<Vector2> newWay = new List<Vector2>();
            
            // Создаю врага
            int randWay = Random.Range(0, wayPoints.Count);
            int randEnemy = Random.Range(0, prefabsEnemy.Length);
            int randPosition = Random.Range(0, pointsSpawner.Length);
            Vector3 positionEnemy = pointsSpawner[randPosition].transform.position;
            positionEnemy.x = Random.Range(-7.55f, -5.45f);
            
            var enemy = Instantiate(prefabsEnemy[randEnemy], positionEnemy, transform.rotation);
            
            
            // Нахожу все точки у конкретного пути
            foreach (Transform child in wayPoints[randWay].transform)
            {
                newWay.Add(child.position);
            }
            
            // Добавляю новый путь врагу
            enemy.GetComponent<Enemy>().path = newWay;

            enemies.Add(enemy.GetComponent<Enemy>());
            spawnAmount -= 1;
            StartCoroutine(SpawnEnemy());
        }

        Debug.Log("Все враги появились!");
    }

    void EnemyDie(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log("Врагов осталось - " + enemies.Count);

            if (enemies.Count == 0)
            {
                if (LevelLogic.instance.currentWave == 3)
                {
                    Debug.Log("Все волны пройдены!");
                }
                else
                {
                    StartCoroutine(ShowNewWave());
                }
            }
        }
    }

    IEnumerator ShowNewWave()
    {
        spawnAmount = countEnemy;
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnEnemy());
        LevelLogic.instance.ShowNewWave();
    }
}