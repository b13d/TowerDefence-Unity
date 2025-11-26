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
    public int maxEnemyLevel;
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
        spawnAmount = maxEnemyLevel;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1f);

        if (spawnAmount > 0)
        {
            List<Vector2> newWay = new List<Vector2>();
            int randWay = Random.Range(0, wayPoints.Count);
            
            // Создаю врага
            // int randWay = Random.Range(0, wayPoints.Count);
            int randEnemy = Random.Range(0, prefabsEnemy.Length);
            // int randPosition = Random.Range(0, pointsSpawner.Length);
            Vector3 positionEnemy = pointsSpawner[randWay].transform.position;
            positionEnemy.x = Random.Range(positionEnemy.x - 1f, positionEnemy.x + 1f);
            
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
    }

    void EnemyDie(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log("Врагов осталось - " + enemies.Count);

            if (enemies.Count == 0)
            {
                if (LevelLogic.instance.currentWave == LevelLogic.instance.GetCountWave)
                {
                    Debug.Log("Все волны пройдены!");
                    LevelLogic.instance.FinishedLevel();
                }
                else if (spawnAmount == 0)
                {
                    Debug.LogError("Новая волна врагов!!");
                    StartCoroutine(ShowNewWave());
                }
            }
        }
    }

    IEnumerator ShowNewWave()
    {
        spawnAmount = maxEnemyLevel;
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnEnemy());
        LevelLogic.instance.ShowNewWave();
    }
}