using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    
    public Transform[] spawnPoints;
    public GameObject zombiePrefab;
    
    private LevelPhase currentPhase;
    private float spawnTimer;
    private int zombiesSpawnedInWave;
    private int currentWaveInPhase;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartPhase(LevelPhase phase)
    {
        currentPhase = phase;
        currentWaveInPhase = 0;
        StartWave();
    }
    
    void StartWave()
    {
        currentWaveInPhase++;
        zombiesSpawnedInWave = 0;
        spawnTimer = 0;
    }
    
    void Update()
    {
        if (currentPhase == null) return;
        
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= currentPhase.spawnInterval && zombiesSpawnedInWave < currentPhase.zombiesPerWave)
        {
            SpawnZombie();
            spawnTimer = 0;
        }
        
        CheckWaveComplete();
    }
    
    void SpawnZombie()
    {
        if (currentPhase.zombieTypes.Length == 0) return;
        
        ZombieData zombieData = GetRandomZombie();
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        GameObject zombieObject = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
        Zombie zombie = zombieObject.GetComponent<Zombie>();
        zombie.Initialize(zombieData);
        
        zombiesSpawnedInWave++;
        EventManager.Instance.Trigger(GameEvents.ZOMBIE_SPAWNED, zombie);
    }
    
    ZombieData GetRandomZombie()
    {
        float totalWeight = 0;
        foreach (ZombieData zombie in currentPhase.zombieTypes)
        {
            totalWeight += zombie.spawnWeight;
        }
        
        float randomValue = Random.Range(0, totalWeight);
        float currentWeight = 0;
        
        foreach (ZombieData zombie in currentPhase.zombieTypes)
        {
            currentWeight += zombie.spawnWeight;
            if (randomValue <= currentWeight)
            {
                return zombie;
            }
        }
        
        return currentPhase.zombieTypes[0];
    }
    
    void CheckWaveComplete()
    {
        if (zombiesSpawnedInWave >= currentPhase.zombiesPerWave && 
            GameObject.FindObjectsOfType<Zombie>().Length == 0)
        {
            if (currentWaveInPhase >= currentPhase.waves)
            {
                LevelManager.Instance.OnWaveComplete();
            }
            else
            {
                StartWave();
            }
        }
    }
}
