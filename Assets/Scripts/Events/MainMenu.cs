using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    
    public void GoToLevels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Settings(bool isOpen)
    {
        settingsMenu.SetActive(isOpen);
    }
}
