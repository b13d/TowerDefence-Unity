using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Game/Tower Data")]
public class TowerData : ScriptableObject
{
    public int level;
    public int damage;
    public int health;
    public int money;
    public string nameTower;
}
