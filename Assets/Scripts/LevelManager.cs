using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] GameObject notificationPrefab;

    [SerializeField] private UIManager uiManager;

    public LevelData currentLevel;
    public List<EnemyDataSO> enemy;
    public List<EnemyDataSO> rareEnemy;
    public GameObject spawnerEnemy;
    public GameObject winWindow;
    public GameObject windowLose;
    public GameObject containerNotifications;
    float _increaseDamageEnemy;
    float _increaseSpeedEnemy;
    float _increaseGoldEnemy;


    [SerializeField] private int counterEnemy = 0;

    public float IncreaseDamageEnemy => _increaseDamageEnemy;
    public float IncreaseSpeedEnemy => _increaseSpeedEnemy;
    public float IncreaseGoldEnemy => _increaseGoldEnemy;

    private void Awake()
    {
        if (LevelSelector.Instance != null)
        {
            currentLevel = LevelSelector.Instance.selectedLevel;
            Instance = this;
        }
        else
        {
            Debug.LogError("Не правильно запущена сцена!");
            Destroy(gameObject);
        }

        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void StartLevel()
    {
        GameManager.Instance.InitialLevels(currentLevel);
        uiManager.Init();

        Time.timeScale = 1;
        Debug.Log("Текущий уровень - " + currentLevel);

        counterEnemy = currentLevel.enemyCount;

        // Инициализация данных в игре
        // GameManager.Instance.levelManager = this;
        _increaseDamageEnemy = currentLevel.increaseDamageEnemy;
        _increaseGoldEnemy = currentLevel.increaseGoldEnemy;
        _increaseSpeedEnemy = currentLevel.increaseSpeedEnemy;
        GameManager.Instance.OnDeadEnemy += CheckLastEnemy;
        GameManager.Instance.money = currentLevel.moneyPlayer;
        GameManager.Instance.health = currentLevel.healthPlayer;
        StartCoroutine(SpawnEnemy());
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDeadEnemy -= CheckLastEnemy;
        }
    }

    public void CompleteGame()
    {
        GameManager.Instance.counterDeadEnemy = 0;
        SceneManager.LoadScene("Levels");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        windowLose.SetActive(true);
    }

    void CheckLastEnemy(int countEnemyDead)
    {
        Debug.Log($"countEnemyLevel === {currentLevel.enemyCount} and count dead {countEnemyDead}");

        if (countEnemyDead == currentLevel.enemyCount)
        {
            Time.timeScale = 0;
            Debug.Log("Последний враг погиб!");
            winWindow.SetActive(true);
        }
    }

    IEnumerator SpawnEnemy()
    {
        Debug.Log($"Спавн врагов! counterEnemy: {counterEnemy}");

        int countDifferentEnemy = enemy.Count;
        Vector3 positionSpawn = spawnerEnemy.transform.position;
        positionSpawn.y = 0f;

        while (counterEnemy > 0)
        {
            if (counterEnemy == 5)
            {
                Notification("Осталось 5 врагов!", TypeMessage.Warning);
            }

            float randTimeWait = Random.Range(0.35f, 1.5f);

            if (Random.Range(0, 100) < 10)
            {
                SpawnRareEnemy(countDifferentEnemy, positionSpawn);
            }
            else
            {
                if (Random.Range(0, 100) < 5)
                {
                    // множественный спавн

                    int countEnemySpawn = Random.Range(1, counterEnemy);

                    if (countEnemySpawn > 3)
                    {
                        countEnemySpawn = 3;
                    }

                    for (int i = 0; i < countEnemySpawn; i++)
                    {
                        SpawnDefaultEnemy(countDifferentEnemy, positionSpawn);
                    }
                }
                else
                {
                    SpawnDefaultEnemy(countDifferentEnemy, positionSpawn);
                }
            }

            yield return new WaitForSeconds(randTimeWait);
        }
    }

    void SpawnDefaultEnemy(int countDifferentEnemy, Vector3 positionSpawn)
    {
        counterEnemy--;
        int indexEnemy = Random.Range(0, countDifferentEnemy);
        var newEnemy = Instantiate(enemy[indexEnemy].enemyPrefab, positionSpawn,
            Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetPoints(currentLevel.wayPointsEnemies);
    }

    public void Notification(string message, TypeMessage typeMessage)
    {
        int extra = containerNotifications.transform.childCount - 3;

        for (int i = 0; i < extra; i++)
        {
            Destroy(containerNotifications.transform.GetChild(0).gameObject);
        }

        GameObject notificationUI = Instantiate(notificationPrefab, containerNotifications.transform);
        Notification not = notificationUI.GetComponent<Notification>();
        not.PropertyMessage = typeMessage;
        not.Message = message;
    }

    void SpawnRareEnemy(int countDifferentEnemy, Vector3 positionSpawn)
    {
        counterEnemy--;
        int indexEnemy = Random.Range(0, countDifferentEnemy);
        var newEnemy = Instantiate(rareEnemy[indexEnemy].enemyPrefab, positionSpawn,
            Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetPoints(currentLevel.wayPointsEnemies);
    }
}