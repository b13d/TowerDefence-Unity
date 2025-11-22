using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerValues
{
    public int health;
    public int money;
}

public class LevelLogic : MonoBehaviour
{
    public static LevelLogic instance;

    const int PRICE = 10;

    [Header("Level Objects")] [SerializeField]
    Texts texts;

    [SerializeField] GameObject _windowLose;

    [SerializeField] GameObject _windowWin;

    [SerializeField] TextMeshProUGUI textCounterWave;

    [SerializeField] GameObject _canvasStartGame;

    [SerializeField] TextMeshProUGUI _txtTargetEnemyOnLevel;

    [SerializeField] TextMeshProUGUI _txtCurrentLevel;

    [SerializeField] GameObject _enemySoundPrefab;

    [SerializeField] public PlayerValues playerValues = new PlayerValues();

    [Header("Level Values")] public int allCounterEnemyDie = 0;
    public int enemyKill = 0;
    public bool pause;
    public int currentWave = 0;
    public int targetEnemyOnLevel;

    [SerializeField] int _currentLevel;
    [SerializeField] int countSpawnersOnLevel;

    private string language = "";

    #region Propetries

    public string SetTextCountTargetEnemy
    {
        set { _txtTargetEnemyOnLevel.text = value; }
    }

    public int GetCurrentLevel => _currentLevel;

    public int GetCountSpawners => countSpawnersOnLevel;

    #endregion


    #region Methods

    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            InitialValues();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel()
    {
        StartCoroutine(AlphaCanvas());
        StartCoroutine(StartLevelWait());
    }

    public void InitialValues()
    {
        language = LocalizationSettings.SelectedLocale.Identifier.Code;
        
        _txtCurrentLevel.text = $"Level - {_currentLevel}";
        texts.txtMoney.text = $"<sprite=0> {playerValues.money}";
        texts.txtHealth.text = $"<sprite=0> {playerValues.health}";
        // counterWave.SetActive(true);

        currentWave++;
        Invoke("HideNewWave", 1f);

        pause = true;
        
        if (language == "en")
        {
            textCounterWave.text = $"Round\r\n{currentWave}/{3}";
        }
        else
        {
            textCounterWave.text = $"Волна\r\n{currentWave}/{3}";
        }

        Time.timeScale = 0.0f;
    }

    public void ShowNewWave()
    {
        currentWave++;
        

        if (language == "en")
        {
            textCounterWave.text = $"Round\r\n{currentWave}/{3}";
        }
        else
        {
            textCounterWave.text = $"Волна\r\n{currentWave}/{3}";
        }
        
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
        // GameManager.instance.ResetData();

        // level scene loading
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        GameManager.instance.SaveGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ShowSettings()
    {
        // Settings.instance.ShowSettings();
    }

    public void GoToLevels()
    {
        SceneManager.LoadScene("Levels");
    }

    #endregion


    #region Coroutines

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

    #endregion
}