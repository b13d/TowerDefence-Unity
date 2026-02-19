using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] List<LevelData> levels;
    [SerializeField] private GameObject prefabButtonLevel;
    
    void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        { 
            var button = Instantiate(prefabButtonLevel, transform);
            ButtonLevel buttonLevel = button.GetComponent<ButtonLevel>();
            buttonLevel.LevelData = levels[i];
        }   
    }

}
