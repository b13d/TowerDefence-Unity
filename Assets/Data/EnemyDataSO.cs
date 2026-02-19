using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyDataSO: ScriptableObject
{
    public GameObject enemyPrefab;
    public int damage;
    public int health;
    public int minSpeed;
    public int maxSpeed;

    [Header("Coins Drop")] public int coinsMin;
    public int coinsMax;

    public int GetRandomCoins()
    {
        return Random.Range(coinsMin, coinsMax + 1);
    }
}