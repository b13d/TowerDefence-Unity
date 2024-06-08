using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int PRICE = 10;

    [SerializeField] int _health;
    [SerializeField] int _money;

    [SerializeField] GameObject _windowLose;

    public Spawner spawnerEnemy;

    public int _finishedEnemy;
    int _enemyKill = 0;

    public static GameManager instance;


    void Start()
    {
        instance = this;
        _health = 100;
        _money = 20;

        Time.timeScale = 1;
    }

    public void UpdateMoney(int levelEnemy)
    {
        _money += PRICE * levelEnemy;

        //_txtMoney.text = $"Money: {_money}";
    }

    public void UpdateHealth()
    {
        _health -= 10;

        //_txtHealth.text = $"Health: {_health}";

        if (_health <= 0)
        {
            Time.timeScale = 0;

            _windowLose.SetActive(true);
        }
    }

    public void Restart()
    {
        _windowLose.SetActive(false);
        //ResetValues();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpeedGame(int speedGame)
    {
        Time.timeScale = speedGame;
    }

    public void EnemyFinished()
    {
        _finishedEnemy++;

        //_txtFinishedEnemy.text = $"Enemy Finished {_finishedEnemy} / {spawnerEnemy.waveNumber[spawnerEnemy.currentWave]}";

        if (spawnerEnemy.GetCurrentCountEnemy == _finishedEnemy && _health > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Переход на следующую волну!!!");
            }

            spawnerEnemy.NextWave();

            //_txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";

            _finishedEnemy = 0;
        }
    }

    public void EnemyKill()
    {
        _enemyKill++;
        //_txtCountEnemy.text = _enemyKill.ToString();

        EnemyFinished();
    }
}
