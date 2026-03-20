using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector Instance;
    public LevelData selectedLevel;
    public int lastUnlockedLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void SelectLevel(LevelData level)
    {
        selectedLevel = level;

        SceneManager.LoadScene(level.sceneLevel.name);
        // Загрузка уровня...
    }

    public void CheckNextLevel(LevelData level)
    {
        if (level.levelId == lastUnlockedLevel)
        {
            lastUnlockedLevel++;
        }
    }
}
