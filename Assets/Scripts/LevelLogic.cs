using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    const int PRICE = 10;

    [SerializeField]
    Texts texts;

    [SerializeField]
    GameObject _windowLose;

    public Spawner spawnerEnemy;

    public static LevelLogic instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            Time.timeScale = 1.0f;
        } else
        {
            Destroy(gameObject);
        }

        texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy}";
        texts.txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";
    }

    public void UpdateMoney(int levelEnemy)
    {
        GameManager.instance.Money += PRICE * levelEnemy;

        texts.txtMoney.text = $"Money: {GameManager.instance.Money}";
    }

    public void UpdateHealth()
    {
        GameManager.instance.Health -= 10;

        texts.txtHealth.text = $"Health: {GameManager.instance.Health}";

        if (GameManager.instance.Health <= 0)
        {
            Time.timeScale = 0;

            _windowLose.SetActive(true);
        }
    }

    public void Restart()
    {
        _windowLose.SetActive(false);

        GameManager.instance.ResetValue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpeedGame(int speedGame)
    {
        Time.timeScale = speedGame;
    }

    public void EnemyFinished()
    {
        GameManager.instance.finishedEnemy++;

        texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy} / {spawnerEnemy.waveNumber[spawnerEnemy.currentWave]}";

        if (spawnerEnemy.GetCurrentCountEnemy == GameManager.instance.finishedEnemy && GameManager.instance.Health > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Переход на следующую волну!!!");
            }

            spawnerEnemy.NextWave();

            texts.txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";

            GameManager.instance.finishedEnemy = 0;
        }
    }

    public void EnemyKill()
    {
        GameManager.instance.enemyKill++;
        texts.txtCountEnemy.text = GameManager.instance.enemyKill.ToString();

        EnemyFinished();
    }

}
