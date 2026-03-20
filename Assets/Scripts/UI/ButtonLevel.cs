using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] private TextMeshProUGUI textLevel;

    private void Start()
    {
    }

    public LevelData LevelData
    {
        get => levelData;
        set
        {
            levelData = value;
            textLevel.text = value.levelId.ToString();
            
            if (LevelSelector.Instance.lastUnlockedLevel < value.levelId)
            {
                GetComponent<Button>().interactable = false;
                GetComponent<CanvasGroup>().alpha = 0.1f;
            }
        }
    }


    public void SelectLevel()
    {
        LevelSelector.Instance.SelectLevel(levelData);
    }
}