using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRoutes : MonoBehaviour
{
    public void LoadLevel(int idLevel)
    {
        if (idLevel == 2)
        {
            GameManager.instance.ResetData();
        }

        GameManager.instance.LoadGame();
        SceneManager.LoadScene(idLevel);
    }
}
