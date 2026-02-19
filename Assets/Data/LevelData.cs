using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum LevelType
{
    Normal,
    Boss,
    Bonus
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Base Info")] public int levelId;
    public int healthPlayer;
    public int moneyPlayer;
    public LevelType levelType;
    public SceneAsset sceneLevel;

    [Header("Gameplay")] 

    public int enemyCount;
    public List<Vector3> wayPointsEnemies;

    [Header("Increase")] 
    public float increaseDamageEnemy;
    public float increaseSpeedEnemy;
    public float increaseGoldEnemy;
    
    [Header("Rewards")] public int coinsReward;

    [Header("Environment")] public Sprite background;
    public Color ambientColor;
}