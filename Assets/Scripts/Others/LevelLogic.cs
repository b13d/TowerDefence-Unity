using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerValues
{
    public int health;
    public int money;
}

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
    GameObject _canvasStartGame;

    [SerializeField]
    TextMeshProUGUI _txtTargetEnemyOnLevel;

    [SerializeField]
    TextMeshProUGUI _txtCurrentLevel;

    [SerializeField]
    GameObject _enemySoundPrefab;

    public int allCounterEnemyDie = 0;
    public int enemyKill = 0;
    public bool pause;
    public int currentWave = 0;
    public int targetEnemyOnLevel;

    [SerializeField]
    public PlayerValues playerValues = new PlayerValues();

    [SerializeField]
    int _currentLevel;

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

            Time.timeScale = 0.0f;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public string SetTextCountTargetEnemy
    {
        set { _txtTargetEnemyOnLevel.text = value; }
    }

    public void StartLevel()
    {
        StartCoroutine(AlphaCanvas());
        StartCoroutine(StartLevelWait());
    }

    IEnumerator AlphaCanvas()
    {
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSecondsRealtime(.1f);

            _canvasStartGame.GetComponent<CanvasGroup>().alpha -= .25f;
        }
    }

    IEnumerator StartLevelWait()
    {
        yield return new WaitForSecondsRealtime(.5f);

        _canvasStartGame.SetActive(false);
        Time.timeScale = 1.0f;
        pause = false;
    }

    public void InitialValues()
    {
        _txtCurrentLevel.text = $"Level - {_currentLevel}";
        _newWave.SetActive(true);
        texts.txtMoney.text = $"<sprite=0> {playerValues.money}";
        //texts.txtFinishedEnemy.text = $"Enemy Finished {GameManager.instance.finishedEnemy}";
        texts.txtHealth.text = $"<sprite=0> {playerValues.health}";

        currentWave++;
        texts.txtCountWave.text = $"{currentWave}";
        Invoke("HideNewWave", 1f);
    }

    public void ShowNewWave()
    {
        Debug.LogError("Показал новую волну!");

        _newWave.SetActive(true);

        currentWave++;
        texts.txtCountWave.text = $"{currentWave}";
        Invoke("HideNewWave", 1f);
    }

    void HideNewWave()
    {
        _newWave.SetActive(false);
    }

    public void ProfitMoney(int levelEnemy)
    {
        playerValues.money += PRICE * Mathf.FloorToInt(levelEnemy + (float)_currentLevel / 2);

        texts.txtMoney.text = $"<sprite=0> {playerValues.money}";

        Invoke("SpawnSoundEnemyDie", .5f);
    }

    void SpawnSoundEnemyDie() 
    {
        var newSoundEnemyDie = Instantiate(_enemySoundPrefab, transform.position, Quaternion.identity);

        Destroy(newSoundEnemyDie, 1f);
    }

    public void UpdateTextMoney()
    {
        texts.txtMoney.text = $"<sprite=0> {playerValues.money}";
    }

    public void HitCastleHealth()
    {
        playerValues.health -= 10;

        texts.txtHealth.text = $"<sprite=0> {playerValues.health}";

        if (playerValues.health <= 0)
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
        allCounterEnemyDie++;
        enemyKill++;
        texts.txtCountEnemy.text = enemyKill.ToString();
    }

    public void FinishedLevel()
    {
        Time.timeScale = 0.0f;

        _windowWin.SetActive(true);

        if (GameManager.instance.Level < _currentLevel)
        {
            GameManager.instance.Level = _currentLevel;
        }

        GameManager.instance.SaveGame();
    }

    public void GameOver()
    {
        GameManager.instance.ResetData();

        // загрузка сцены с уровнями
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        GameManager.instance.SaveGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int GetCountSpawners
    {
        get { return countSpawnersOnLevel; }
    }

    public void ShowSettings()
    {
        Settings.instance.ShowSettings();
    }
}
