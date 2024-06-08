using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    [SerializeField] int _health;
    [SerializeField] int _money;

    public int finishedEnemy;
    public int enemyKill = 0;

    public static GameManager instance;


    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }


    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }


    void Start()
    {
        if (instance == null)
        {
            instance = this;

            _health = 100;
            _money = 20;

            Time.timeScale = 1;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ResetValue()
    {
        _health = 100;
        _money = 20;
        finishedEnemy = 0;
        enemyKill = 0;
}

    
}
