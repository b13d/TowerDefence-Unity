using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI recordText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moneyText;
    public LevelManager levelManager;
    
    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnHealthChanged += UpdateHealth;
        GameManager.Instance.OnMoneyChanged += UpdateMoney;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnHealthChanged-= UpdateHealth;
            GameManager.Instance.OnMoneyChanged -= UpdateMoney;
        }
    }
    
    void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    void UpdateHealth(int health)
    {
        healthText.text = health.ToString();

        if (health <= 0)
        {
            levelManager.GameOver();
        }
    }
    
    void UpdateMoney(int money)
    {
        moneyText.text = money.ToString();
    }
    
    public void Init()
    {
        healthText.text = GameManager.Instance.health.ToString();
        recordText.text = GameManager.Instance.record.ToString();
        scoreText.text = GameManager.Instance.score.ToString();
        moneyText.text = GameManager.Instance.money.ToString();
    }
}
