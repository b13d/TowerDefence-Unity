using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int record;
    public int money;
    public int health = 100;
    public int wave;
    public int counterDeadEnemy;

    [SerializeField] private int criticalLowHealth = 10;

    public static GameManager Instance;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnMoneyChanged;
    public event Action<int> OnWaveChanged;
    public event Action<int> OnDeadEnemy;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("В сцене больше одного GameManager!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddDamage(int damage)
    {
        health -= damage;

        if (health < criticalLowHealth && health > 0)
        {
            LevelManager.Instance.Notification("У игрока осталось мало здоровья!", TypeMessage.Error);
        }

        if (health <= 0)
        {
            health = 0;
            Time.timeScale = 0;
        }

        OnHealthChanged?.Invoke(health);
    }

    public void AddScore()
    {
        score += 1;
        OnScoreChanged?.Invoke(score);
    }

    public void AddMoney(int moneyEnemy)
    {
        money += moneyEnemy;
        OnMoneyChanged?.Invoke(money);
    }

    public void BuyTower(int wasteMoney)
    {
        money -= wasteMoney;
        OnMoneyChanged?.Invoke(money);
    }

    public void NextWave()
    {
        wave += 1;
        OnWaveChanged?.Invoke(wave);
    }


    public void DeadEnemy()
    {
        counterDeadEnemy++;

        OnDeadEnemy?.Invoke(counterDeadEnemy);
    }

    public void InitialLevels(LevelData levelData)
    {
        score = 0;
        record = 0;
        money = levelData.moneyPlayer;
        health = levelData.healthPlayer;
        wave = 0;
        counterDeadEnemy = 0;
    }
}