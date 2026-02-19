using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Game/Level Database")]

public class LevelDatabase : ScriptableObject
{
    public List<LevelData> levels;

    public LevelData GetLevel(int id)
    {
        return levels.Find(l => l.levelId == id);
    }
}
