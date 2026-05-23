using UnityEngine;

[CreateAssetMenu(fileName = "NewZombie", menuName = "PVZ_Arknights/Zombie")]
public class ZombieData : ScriptableObject
{
    public string zombieName;
    public Sprite sprite;
    public int health;
    public int damage;
    public float speed;
    public float attackSpeed;
    public ZombieType zombieType;
    public EnemyOrigin origin;
    public float spawnWeight;
}

public enum ZombieType
{
    Normal,
    Fast,
    Tank,
    Flying,
    Boss
}

public enum EnemyOrigin
{
    PVZ,
    Arknights
}
