using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CounterWave : MonoBehaviour
{
    LevelData levelData;

    [SerializeField] private int enemyCount;
    [SerializeField] int waveCicle;
    [SerializeField] private int currentWave;
    [SerializeField] private int enemyOnOneWave;
    [SerializeField] private int lastAdditionalEnemy;
    [SerializeField] private GameObject prefabEnemy;

    private TextMeshProUGUI textMesh;
    // [SerializeField] private List<GameObject> enemy;


    public int WaveCicle { get => waveCicle; set => waveCicle = value; }
    public int EnemyWave => enemyOnOneWave;

    void Start()
    {
        levelData = LevelManager.Instance.currentLevel;

        if (levelData == null)
        {
            Debug.LogError("LevelData пуст!");
        }

        currentWave = 1;
        enemyCount = levelData.enemyCount;
        waveCicle = levelData.waveCount;

        textMesh = GetComponent<TextMeshProUGUI>();

        if (enemyCount % waveCicle != 0)
        {
            lastAdditionalEnemy = enemyCount % waveCicle;
        }

        enemyOnOneWave = enemyCount / waveCicle;

        textMesh.text = String.Format("Wave {0} / {1}", currentWave, waveCicle);
        Debug.Log("Количество врагов на каждую волну: " + enemyCount / waveCicle);

        // StartCoroutine(SpawnEnemy());
    }


    // void RemoveIndex()
    // {
    //     
    //     Debug.Log("RemoveIndex func");
    //     enemy.RemoveAt(0);
    //
    //     if (enemy.Count == 0)
    //     {
    //         return;
    //     }
    //     
    //     Debug.Log("Начинаю новое удаление индекса в enemy");
    //     
    //     StartCoroutine(Delay(1.5f, RemoveIndex));
    // }

    public void NextWave(Func<int, IEnumerator> spawnEnemyFn)
    {
        currentWave++;


        if (currentWave > waveCicle)
        {
            Debug.Log("Игрок выиграл!");
            return;
        }
        else
        {
            textMesh.text = String.Format("Wave {0} / {1}", currentWave, waveCicle);
        }

        int enemyCount = enemyOnOneWave;

        if (currentWave == waveCicle)
        {
            enemyCount += lastAdditionalEnemy;
            // добавляем последних врагов, которые не попали при делении на волны
        }

        StartCoroutine(spawnEnemyFn(enemyCount));
    }


    IEnumerator Delay(float delay, Action func)
    {
        yield return new WaitForSeconds(delay);
        func?.Invoke();
    }

    IEnumerator SpawnEnemy()
    {
        // lastAdditionalEnemy = 0;

        // for (int i = 0; i < enemyOnOneWave; i++)
        // {
        //     Vector3 position = new Vector3(Random.Range(-6, 6), 0, Random.Range(-5, 5));
        //     var newEnemy = Instantiate(prefabEnemy, position, Quaternion.identity);
        //     enemy.Add(newEnemy);
        // }

        // yield return new WaitForSeconds(2f);
        //
        // Debug.Log("Очистка врагов началась...");
        //
        // for (int i = 0; i < enemy.Count; i++)
        // {
        //     Destroy(enemy[i]);
        // }
        //
        // enemy.Clear();
        //
        // Debug.Log("Очистка врагов закончилась");
        //
        // currentWave++;
        //

        // if (currentWave < waveCicle)
        // {
        //     StartCoroutine(SpawnEnemy());
        // }
        // else if (currentWave == waveCicle)
        // {
        //     enemyOnOneWave += lastAdditionalEnemy;
        //     StartCoroutine(SpawnEnemy());
        // }
        // else if (currentWave > waveCicle)
        // {
        //     Debug.Log("Враги закончились");
        // }

        yield return null;
    }

    private void OnValidate()
    {
        lastAdditionalEnemy = 0;

        if (enemyCount % waveCicle != 0)
        {
            lastAdditionalEnemy = enemyCount % waveCicle;
            enemyOnOneWave = enemyCount / waveCicle;
        }
    }
}