using UnityEngine;

public class SunlightSpawner : MonoBehaviour
{
    public float spawnInterval = 7.5f;
    public float randomRange = 5f;
    public Transform[] spawnPoints;
    
    private float spawnTimer;
    
    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= spawnInterval)
        {
            SpawnSunlight();
            spawnTimer = 0;
        }
    }
    
    void SpawnSunlight()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector2 offset = new Vector2(Random.Range(-randomRange, randomRange), 0);
        
        GameObject sunlight = Instantiate(Resources.Load<GameObject>("Prefabs/Sunlight/Sunlight"));
        sunlight.transform.position = spawnPoint.position + (Vector3)offset;
    }
}
