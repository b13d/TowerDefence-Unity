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

    [SerializeField]
    GameObject _newWave;

    [SerializeField]
    int _currentLevel;

    public int enemyKill = 0;
    public bool pause;
    public int currentWave = 0;

    [SerializeField]
    int countSpawnersOnLevel;
    

    public static LevelLogic instance;

    public int GetCurrentLevel
    {
        get { return _currentLevel; }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            InitialValues();

            pause = true;

            Time.timeScale = 1.0f;

            Invoke("UnPause", 1.0f);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    void UnPause()
    {
        pause = false;
    }

    public void InitialValues()
    {
        _newWave.SetActive(true);
        texts.txtMoney.text = $"<sprite=0> {GameManager.instance.Money}";
        texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy}";

        Invoke("HideNewWave", 1f);
        //texts.txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";
    }

    public void ShowNewWave()
    {
        _newWave.SetActive(true);

        Invoke("HideNewWave", 1f);
    }

    void HideNewWave()
    {
        _newWave.SetActive(false);

        currentWave++;
    }

    public void ProfitMoney(int levelEnemy)
    {
        GameManager.instance.Money += PRICE * Mathf.FloorToInt(levelEnemy + (float)_currentLevel / 2);

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


    public void EnemyKill()
    {
        enemyKill++;
        texts.txtCountEnemy.text = enemyKill.ToString();
    }

    public void FinishedLevel()
    {
        Time.timeScale = 0.0f;

        _windowWin.SetActive(true);
    }

    public void GameOver()
    {
        GameManager.instance.ResetData();

        // загрузка сцены с уровнями
        SceneManager.LoadScene(1);
    }

    public void NextLevel(int nextLevel)
    {
        //GameManager.instance.Level++;
        GameManager.instance.SaveGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int GetCountSpawners
    {
        get { return countSpawnersOnLevel; }
    }
}
