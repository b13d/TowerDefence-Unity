using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Serializable]
    class SaveData
    {
        public int level;
        public int money;
        public int healthPlayer;
    }
    

    [SerializeField] int _health;
    [SerializeField] int _money;
    [SerializeField] int _level;

    public int finishedEnemy;
    

    public static GameManager instance;


    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

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


    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up / 2);
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up / 2, Color.red);

        if (hit)
        {
            Debug.Log(hit.collider);
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.level = _level;
        data.money = _money;
        data.healthPlayer = _health;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!!");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _level = data.level;
            _money = data.money;
            _health = data.healthPlayer;
            Debug.Log("Game data loaded!!");
        } else
        {
            ResetValue(false);

            Debug.LogError("There is no save data!!");
        }
    }


    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            ResetValue(true);
            Debug.Log("Data reset complete");
        } else
        {
            Debug.LogError("No save data to delete");
        }
    }

    public void ResetValue(bool isFullReset)
    {
        if (isFullReset)
        {
            _level = 0;
        } 

        _health = 100;
        _money = 20;
        finishedEnemy = 0;
    }
}
