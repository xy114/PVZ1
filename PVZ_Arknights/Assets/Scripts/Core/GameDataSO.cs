using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPlantData", menuName = "PVZ_Arknights/PlantData")]
public class PlantDataSO : ScriptableObject
{
    public string plantName;
    public Sprite icon;
    public Sprite defaultSprite;
    public int cost;
    public int health;
    public int damage;
    public float attackSpeed;
    public float range;
    public PlantType plantType;
    public List<PlantSkin> availableSkins = new List<PlantSkin>();
}

[CreateAssetMenu(fileName = "NewZombieData", menuName = "PVZ_Arknights/ZombieData")]
public class ZombieDataSO : ScriptableObject
{
    public string zombieName;
    public Sprite sprite;
    public int health;
    public int damage;
    public float speed;
    public float attackSpeed;
    public ZombieType zombieType;
    public EnemyOrigin origin;
    public float spawnWeight = 1f;
}

[System.Serializable]
public class LevelPhaseSO
{
    public PhaseType phaseType;
    public int waves;
    public float spawnInterval;
    public int zombiesPerWave;
    public List<ZombieDataSO> zombieTypes = new List<ZombieDataSO>();
}

[CreateAssetMenu(fileName = "NewLevelData", menuName = "PVZ_Arknights/LevelData")]
public class LevelDataSO : ScriptableObject
{
    public string levelName;
    public int levelNumber;
    public int difficulty;
    public List<LevelPhaseSO> phases = new List<LevelPhaseSO>();
    public Sprite backgroundSprite;
}
