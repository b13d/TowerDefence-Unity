using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject[] prefabsEnemy;
    public int spawnAmount;
    public int countEnemy;
    public GameObject[] pointsSpawner;
    public GameObject target;
    public List<GameObject> enemies;
    public List<Vector2> pathsList;
    public Vector2[] pathsArray;

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
            // int randPath = Random.Range(0, paths.Count);
            int randEnemy = Random.Range(0, prefabsEnemy.Length);
            int randPosition = Random.Range(0, pointsSpawner.Length);
            var enemy = Instantiate(prefabsEnemy[randEnemy], pointsSpawner[randPosition].transform.position,
                transform.rotation);
            // enemy.GetComponent<Enemy>().path = paths[randPath];
            enemies.Add(enemy);
            spawnAmount -= 1;
            StartCoroutine(SpawnEnemy());
        }

        Debug.Log("Все враги появились!");
    }
}