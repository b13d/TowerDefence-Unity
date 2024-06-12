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

    [SerializeField]
    GameObject _windowWin;


    public int enemyKill = 0;
    public Spawner spawnerEnemy;

    public static LevelLogic instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            InitialValues();
            Time.timeScale = 1.0f;

        } else
        {
            Destroy(gameObject);
        }
    }

    public void InitialValues()
    {
        texts.txtMoney.text = $"<sprite=0> {GameManager.instance.Money}";
        texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy}";
        //texts.txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";
    }

    public void ProfitMoney(int levelEnemy)
    {
        GameManager.instance.Money += PRICE * levelEnemy;

        texts.txtMoney.text = $"<sprite=0> {GameManager.instance.Money}";
    }

    public void UpdateTextMoney()
    {
        texts.txtMoney.text = $"<sprite=0> {GameManager.instance.Money}";
    }

    public void HitCastleHealth()
    {
        GameManager.instance.Health -= 10;

        texts.txtHealth.text = $"<sprite=0> {GameManager.instance.Health}";

        if (GameManager.instance.Health <= 0)
        {
            Time.timeScale = 0;

            _windowLose.SetActive(true);
        }
    }

    public void Restart()
    {
        _windowLose.SetActive(false);

        GameManager.instance.LoadGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ReturnToLevels()
    {
        _windowWin.SetActive(false);

        SceneManager.LoadScene(1);
    }

    public void SpeedGame(int speedGame)
    {
        Time.timeScale = speedGame;
    }

    public void EnemyFinished()
    {
        GameManager.instance.finishedEnemy++;

        //texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy} / {spawnerEnemy.waveNumber[spawnerEnemy.currentWave]}";

        //if (spawnerEnemy.GetCurrentCountEnemy == GameManager.instance.finishedEnemy && GameManager.instance.Health > 0)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Debug.Log("Переход на следующую волну!!!");
        //    }

        //    spawnerEnemy.NextWave();

        //    texts.txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";
        //    texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy} / {spawnerEnemy.waveNumber[spawnerEnemy.currentWave]}";

        //    GameManager.instance.finishedEnemy = 0;
        //}
    }

    public void EnemyKill()
    {
        enemyKill++;
        texts.txtCountEnemy.text = enemyKill.ToString();

        EnemyFinished();
    }

    public void FinishedLevel()
    {
        Time.timeScale = 0.0f;

        _windowWin.SetActive(true);
    }

    public void NextLevel(int nextLevel)
    {
        GameManager.instance.Level++;
        GameManager.instance.SaveGame();

        SceneManager.LoadScene(nextLevel);
    }
}
