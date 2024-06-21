using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelRoutes : MonoBehaviour
{
    [SerializeField] List<GameObject> _levels = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(WaitGameManager());
    }

    IEnumerator WaitGameManager()
    {
        Debug.LogError("Инициализация уровней");

        yield return new WaitForSecondsRealtime(.5f);

        if (GameManager.instance == null)
        {
            StartCoroutine(WaitGameManager());
        } 
        else
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                if (GameManager.instance.Level < i)
                {
                    _levels[i].GetComponent<Image>().color = Color.black;
                    _levels[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    _levels[i].GetComponent<Image>().color = Color.white;
                    _levels[i].GetComponent<Button>().interactable = true;
                }
            }
        }

    }

    public void ShowSettings()
    {
        Settings.instance.ShowSettings();
    }

    public void LoadLevel(int idLevel)
    {
        GameManager.instance.LoadGame();
        SceneManager.LoadScene(idLevel);
    }
}
