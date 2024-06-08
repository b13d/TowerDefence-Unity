using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScene : MonoBehaviour
{
   public void LoadLevelsScene()
    {
        SceneManager.LoadScene(1);
    }
}
