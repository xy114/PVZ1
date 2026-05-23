using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    private Dictionary<string, Action<object[]>> eventDictionary = new Dictionary<string, Action<object[]>>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Subscribe(string eventName, Action<object[]> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
        eventDictionary[eventName] += listener;
    }
    
    public void Unsubscribe(string eventName, Action<object[]> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }
    
    public void Trigger(string eventName, params object[] args)
    {
        if (eventDictionary.TryGetValue(eventName, out Action<object[]> action))
        {
            action?.Invoke(args);
        }
    }
}

public static class GameEvents
{
    public const string PLANT_PLACED = "PlantPlaced";
    public const string ZOMBIE_SPAWNED = "ZombieSpawned";
    public const string ZOMBIE_KILLED = "ZombieKilled";
    public const string SUNLIGHT_COLLECTED = "SunlightCollected";
    public const string WAVE_COMPLETE = "WaveComplete";
    public const string PHASE_CHANGED = "PhaseChanged";
    public const string LEVEL_COMPLETE = "LevelComplete";
    public const string GAME_OVER = "GameOver";
}
