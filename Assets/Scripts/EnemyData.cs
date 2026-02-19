using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab;
    public int damage;
    public int health;
    public int speed;

    [Header("Coins Drop")] public int coinsMin;
    public int coinsMax;

    public int GetRandomCoins()
    {
        return Random.Range(coinsMin, coinsMax + 1);
    }
}