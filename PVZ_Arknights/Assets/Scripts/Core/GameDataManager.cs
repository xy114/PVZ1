using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    
    public List<PlantDataSO> plants = new List<PlantDataSO>();
    public List<ZombieDataSO> zombies = new List<ZombieDataSO>();
    public List<LevelDataSO> levels = new List<LevelDataSO>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void LoadData()
    {
        LoadPlants();
        LoadZombies();
        LoadLevels();
    }
    
    void LoadPlants()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "plants.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlantDataSO[] plantArray = JsonUtility.FromJson<PlantDataArray>(json).plants;
            plants.AddRange(plantArray);
        }
        else
        {
            CreateDefaultPlants();
        }
    }
    
    void LoadZombies()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "zombies.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ZombieDataSO[] zombieArray = JsonUtility.FromJson<ZombieDataArray>(json).zombies;
            zombies.AddRange(zombieArray);
        }
        else
        {
            CreateDefaultZombies();
        }
    }
    
    void LoadLevels()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "levels.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LevelDataSO[] levelArray = JsonUtility.FromJson<LevelDataArray>(json).levels;
            levels.AddRange(levelArray);
        }
        else
        {
            CreateDefaultLevels();
        }
    }
    
    void CreateDefaultPlants()
    {
        plants.Add(new PlantDataSO { plantName = "豌豆射手", cost = 100, health = 100, damage = 20, attackSpeed = 1f, range = 5f, plantType = 0 });
        plants.Add(new PlantDataSO { plantName = "向日葵", cost = 50, health = 100, damage = 0, attackSpeed = 0f, range = 0f, plantType = 3 });
        plants.Add(new PlantDataSO { plantName = "坚果", cost = 50, health = 400, damage = 0, attackSpeed = 0f, range = 0f, plantType = 1 });
        plants.Add(new PlantDataSO { plantName = "寒冰射手", cost = 175, health = 100, damage = 20, attackSpeed = 1f, range = 5f, plantType = 0 });
        plants.Add(new PlantDataSO { plantName = "樱桃炸弹", cost = 150, health = 100, damage = 1800, attackSpeed = 0f, range = 1.5f, plantType = 0 });
        plants.Add(new PlantDataSO { plantName = "双重射手", cost = 200, health = 100, damage = 20, attackSpeed = 1f, range = 5f, plantType = 0 });
    }
    
    void CreateDefaultZombies()
    {
        zombies.Add(new ZombieDataSO { zombieName = "普通僵尸", health = 100, damage = 10, speed = 1f, attackSpeed = 1f, zombieType = 0, origin = 0, spawnWeight = 1f });
        zombies.Add(new ZombieDataSO { zombieName = "路障僵尸", health = 200, damage = 10, speed = 1f, attackSpeed = 1f, zombieType = 0, origin = 0, spawnWeight = 0.5f });
        zombies.Add(new ZombieDataSO { zombieName = "铁桶僵尸", health = 400, damage = 10, speed = 0.8f, attackSpeed = 1f, zombieType = 2, origin = 0, spawnWeight = 0.3f });
        zombies.Add(new ZombieDataSO { zombieName = "快速僵尸", health = 70, damage = 10, speed = 2f, attackSpeed = 1.5f, zombieType = 1, origin = 0, spawnWeight = 0.4f });
        zombies.Add(new ZombieDataSO { zombieName = "源石虫", health = 50, damage = 5, speed = 1.5f, attackSpeed = 1f, zombieType = 1, origin = 1, spawnWeight = 0.6f });
        zombies.Add(new ZombieDataSO { zombieName = "萨卡兹战士", health = 300, damage = 25, speed = 1f, attackSpeed = 1f, zombieType = 0, origin = 1, spawnWeight = 0.4f });
    }
    
    void CreateDefaultLevels()
    {
        LevelDataSO level1 = new LevelDataSO();
        level1.levelName = "切尔诺伯格外围";
        level1.levelNumber = 1;
        level1.difficulty = 1;
        level1.phases = new LevelPhaseSO[]
        {
            new LevelPhaseSO { phaseType = 0, waves = 2, spawnInterval = 3f, zombiesPerWave = 3, zombieIndices = new int[] { 0 } },
            new LevelPhaseSO { phaseType = 1, waves = 2, spawnInterval = 2.5f, zombiesPerWave = 4, zombieIndices = new int[] { 0, 3 } },
            new LevelPhaseSO { phaseType = 2, waves = 2, spawnInterval = 2f, zombiesPerWave = 5, zombieIndices = new int[] { 0, 1, 3 } }
        };
        
        LevelDataSO level2 = new LevelDataSO();
        level2.levelName = "龙门市区";
        level2.levelNumber = 2;
        level2.difficulty = 2;
        level2.phases = new LevelPhaseSO[]
        {
            new LevelPhaseSO { phaseType = 0, waves = 2, spawnInterval = 2.5f, zombiesPerWave = 4, zombieIndices = new int[] { 0, 4 } },
            new LevelPhaseSO { phaseType = 1, waves = 3, spawnInterval = 2f, zombiesPerWave = 5, zombieIndices = new int[] { 0, 1, 3, 4 } },
            new LevelPhaseSO { phaseType = 2, waves = 3, spawnInterval = 1.5f, zombiesPerWave = 6, zombieIndices = new int[] { 1, 2, 3, 5 } }
        };
        
        LevelDataSO level3 = new LevelDataSO();
        level3.levelName = "乌萨斯荒野";
        level3.levelNumber = 3;
        level3.difficulty = 3;
        level3.phases = new LevelPhaseSO[]
        {
            new LevelPhaseSO { phaseType = 0, waves = 3, spawnInterval = 2f, zombiesPerWave = 5, zombieIndices = new int[] { 0, 4 } },
            new LevelPhaseSO { phaseType = 1, waves = 3, spawnInterval = 1.5f, zombiesPerWave = 6, zombieIndices = new int[] { 1, 3, 4, 5 } },
            new LevelPhaseSO { phaseType = 2, waves = 3, spawnInterval = 1f, zombiesPerWave = 8, zombieIndices = new int[] { 2, 3, 5 } }
        };
        
        levels.Add(level1);
        levels.Add(level2);
        levels.Add(level3);
    }
    
    public PlantDataSO GetPlantByName(string name)
    {
        return plants.Find(p => p.plantName == name);
    }
    
    public ZombieDataSO GetZombieByName(string name)
    {
        return zombies.Find(z => z.zombieName == name);
    }
    
    public LevelDataSO GetLevelByNumber(int number)
    {
        return levels.Find(l => l.levelNumber == number);
    }
}

[System.Serializable]
public class PlantDataSO
{
    public string plantName;
    public int cost;
    public int health;
    public int damage;
    public float attackSpeed;
    public float range;
    public int plantType;
}

[System.Serializable]
public class ZombieDataSO
{
    public string zombieName;
    public int health;
    public int damage;
    public float speed;
    public float attackSpeed;
    public int zombieType;
    public int origin;
    public float spawnWeight;
}

[System.Serializable]
public class LevelPhaseSO
{
    public int phaseType;
    public int waves;
    public float spawnInterval;
    public int zombiesPerWave;
    public int[] zombieIndices;
}

[System.Serializable]
public class LevelDataSO
{
    public string levelName;
    public int levelNumber;
    public int difficulty;
    public LevelPhaseSO[] phases;
}

[System.Serializable]
public class PlantDataArray
{
    public PlantDataSO[] plants;
}

[System.Serializable]
public class ZombieDataArray
{
    public ZombieDataSO[] zombies;
}

[System.Serializable]
public class LevelDataArray
{
    public LevelDataSO[] levels;
}
