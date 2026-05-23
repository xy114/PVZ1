using UnityEngine;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    [Header("Game Data")]
    public List&lt;PlantDataSO&gt; plants = new List&lt;PlantDataSO&gt;();
    public List&lt;ZombieDataSO&gt; zombies = new List&lt;ZombieDataSO&gt;();
    public List&lt;LevelDataSO&gt; levels = new List&lt;LevelDataSO&gt;();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDefaultData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDefaultData()
    {
        if (plants.Count == 0)
        {
            Debug.LogWarning("GameDataManager: No plants assigned!");
        }
        
        if (zombies.Count == 0)
        {
            Debug.LogWarning("GameDataManager: No zombies assigned!");
        }
        
        if (levels.Count == 0)
        {
            Debug.LogWarning("GameDataManager: No levels assigned!");
        }
    }

    public PlantDataSO GetPlantByName(string name)
    {
        return plants.Find(p =&gt; p.plantName == name);
    }

    public ZombieDataSO GetZombieByName(string name)
    {
        return zombies.Find(z =&gt; z.zombieName == name);
    }

    public LevelDataSO GetLevelByNumber(int number)
    {
        return levels.Find(l =&gt; l.levelNumber == number);
    }
}
