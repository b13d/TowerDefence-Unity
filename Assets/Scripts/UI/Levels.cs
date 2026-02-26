using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField] List<LevelData> levels;
    [SerializeField] private GameObject prefabButtonLevel;
    [SerializeField] private GameObject parentSpawn;
    
    void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        { 
            var button = Instantiate(prefabButtonLevel, parentSpawn.transform);
            ButtonLevel buttonLevel = button.GetComponent<ButtonLevel>();
            button.GetComponent<Image>().sprite = levels[i].background;
            buttonLevel.LevelData = levels[i];
        }   
    }

}
