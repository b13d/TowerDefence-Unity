using TMPro;
using UnityEngine;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] private TextMeshProUGUI textLevel;

    public LevelData LevelData
    {
        get => levelData;
        set
        {
            levelData = value;
            textLevel.text = "Уровень - " + value.levelId;
        }
    }


    public void SelectLevel()
    {
        LevelSelector.Instance.SelectLevel(levelData);
    }
}