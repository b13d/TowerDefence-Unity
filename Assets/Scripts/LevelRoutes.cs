using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRoutes : MonoBehaviour
{
    public void LoadLevel(int idLevel)
    {
        SceneManager.LoadScene(idLevel);
    }
}
