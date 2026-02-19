using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadSimpleScene()
    {
        Debug.Log("Нажимаю на загрузку сцену Levels");
        SceneManager.LoadScene("Scenes/Levels");
    }

    public void LoadInfiniteScene()
    {
        SceneManager.LoadScene("Scenes/InfiniteScene");
    }
}
