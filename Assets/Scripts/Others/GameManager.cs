using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DG.Tweening;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject prefabMoneyImage;
    public GameObject parentMoney;
    private const int ValueRand = 150;




    [SerializeField] GameObject _clickSound;

    [Serializable]
    class SaveData
    {
        public int level;
        public int money;
        public int healthPlayer;
    }

    [SerializeField] int _level;

    #region Properties

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    #endregion

    #region Methods

    void Awake()
    {
        // StartCoroutine(SpawnMoney());

        if (instance == null)
        {
            instance = this;
            Time.timeScale = 1;
            LoadGame();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevels()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        // Создание звука щелчка в игре и его удаление
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var soundClick = Instantiate(_clickSound, transform);
            Destroy(soundClick, 1f);
        }

        // Перезапуск игры
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GuardiansoftheRealm.dat");
        SaveData data = new SaveData();
        data.level = _level;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!!");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/GuardiansoftheRealm.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GuardiansoftheRealm.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _level = data.level;
            Debug.Log("Game data loaded!!");
        }
        else
        {
            ResetValue(false);

            Debug.LogError("There is no save data!!");
        }
    }


    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/GuardiansoftheRealm.dat"))
        {
            File.Delete(Application.persistentDataPath + "/GuardiansoftheRealm.dat");
            ResetValue(true);
            Debug.Log("Data reset complete");
        }
        else
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
    }

    IEnumerator SpawnMoney()
    {
        while (true)
        {
            float randValueX = Random.Range(-ValueRand, ValueRand);
            float randValueY = Random.Range(-ValueRand, ValueRand);
            var money = Instantiate(prefabMoneyImage, new Vector3(randValueX, randValueY, 0), Quaternion.identity,
                parentMoney.transform);

            money.transform.DOScale(1, 0.5f);

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(DestroyMoney(money));
        }
    }

    IEnumerator DestroyMoney(GameObject money)
    {
        money.transform.DOScale(0, 0.5f);
        yield return new WaitForSeconds(1f);

        Destroy(money);
    }

    #endregion
}