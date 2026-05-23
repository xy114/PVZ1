using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int Sunlight { get; private set; } = 150;
    public List<PlantData> SelectedPlants { get; private set; } = new List<PlantData>();
    public int CurrentLevel { get; private set; } = 1;
    public bool IsPaused { get; private set; } = false;
    
    public delegate void SunlightChanged(int amount);
    public event SunlightChanged OnSunlightChanged;
    
    public delegate void GameStateChanged(GameState state);
    public event GameStateChanged OnGameStateChanged;
    
    private GameState currentState;
    
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
    
    public void AddSunlight(int amount)
    {
        Sunlight += amount;
        OnSunlightChanged?.Invoke(Sunlight);
    }
    
    public bool RemoveSunlight(int amount)
    {
        if (Sunlight >= amount)
        {
            Sunlight -= amount;
            OnSunlightChanged?.Invoke(Sunlight);
            return true;
        }
        return false;
    }
    
    public void SetGameState(GameState state)
    {
        currentState = state;
        OnGameStateChanged?.Invoke(state);
        
        if (state == GameState.Paused)
        {
            IsPaused = true;
            Time.timeScale = 0;
        }
        else if (state == GameState.Battle)
        {
            IsPaused = false;
            Time.timeScale = 1;
        }
    }
    
    public void SelectPlant(PlantData plant)
    {
        if (!SelectedPlants.Contains(plant) && SelectedPlants.Count < 6)
        {
            SelectedPlants.Add(plant);
        }
    }
    
    public void DeselectPlant(PlantData plant)
    {
        SelectedPlants.Remove(plant);
    }
    
    public void ClearSelectedPlants()
    {
        SelectedPlants.Clear();
    }
    
    public void SetLevel(int level)
    {
        CurrentLevel = level;
    }
    
    public void ResetSunlight()
    {
        Sunlight = 150;
        OnSunlightChanged?.Invoke(Sunlight);
    }
}

public enum GameState
{
    Menu,
    LevelSelect,
    CharacterSelect,
    Battle,
    Paused,
    GameOver,
    Victory
}
