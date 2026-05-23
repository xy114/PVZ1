using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour
{
    public Text sunlightText;
    public Text waveText;
    public Text phaseText;
    public Button pauseButton;
    public Button restartButton;
    public Transform plantPanel;
    public GameObject plantSlotPrefab;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    
    private Dictionary<PlantData, PlantSlotUI> plantSlots = new Dictionary<PlantData, PlantSlotUI>();
    
    void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButton);
        restartButton.onClick.AddListener(OnRestartButton);
        
        GameManager.Instance.OnSunlightChanged += UpdateSunlight;
        EventManager.Instance.Subscribe(GameEvents.WAVE_COMPLETE, OnWaveComplete);
        EventManager.Instance.Subscribe(GameEvents.PHASE_CHANGED, OnPhaseChanged);
        EventManager.Instance.Subscribe(GameEvents.LEVEL_COMPLETE, OnLevelComplete);
        EventManager.Instance.Subscribe(GameEvents.GAME_OVER, OnGameOver);
        
        GameManager.Instance.SetGameState(GameState.Battle);
        InitializePlantPanel();
        UpdateSunlight(GameManager.Instance.Sunlight);
    }
    
    void InitializePlantPanel()
    {
        foreach (PlantData plant in GameManager.Instance.SelectedPlants)
        {
            GameObject slot = Instantiate(plantSlotPrefab, plantPanel);
            PlantSlotUI slotUI = slot.GetComponent<PlantSlotUI>();
            slotUI.Initialize(plant);
            plantSlots[plant] = slotUI;
        }
    }
    
    void UpdateSunlight(int amount)
    {
        sunlightText.text = amount.ToString();
    }
    
    void OnWaveComplete(object[] args)
    {
        int currentWave = (int)args[0];
        int totalWaves = (int)args[1];
        waveText.text = $"波次 {currentWave}/{totalWaves}";
    }
    
    void OnPhaseChanged(object[] args)
    {
        PhaseType phase = (PhaseType)args[0];
        string phaseStr = "";
        switch (phase)
        {
            case PhaseType.Early: phaseStr = "前期"; break;
            case PhaseType.Mid: phaseStr = "中期"; break;
            case PhaseType.Late: phaseStr = "后期"; break;
        }
        phaseText.text = phaseStr;
    }
    
    void OnLevelComplete()
    {
        victoryPanel.SetActive(true);
    }
    
    void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
    void OnPauseButton()
    {
        if (GameManager.Instance.IsPaused)
        {
            GameManager.Instance.SetGameState(GameState.Battle);
        }
        else
        {
            GameManager.Instance.SetGameState(GameState.Paused);
        }
    }
    
    void OnRestartButton()
    {
        SceneLoader.Instance.ReloadCurrentScene();
    }
    
    public void OnVictoryContinue()
    {
        SceneLoader.Instance.LoadScene(SceneName.LevelSelect);
    }
    
    public void OnGameOverRetry()
    {
        SceneLoader.Instance.ReloadCurrentScene();
    }
    
    public void OnGameOverQuit()
    {
        SceneLoader.Instance.LoadScene(SceneName.MainMenu);
    }
}

public class PlantSlotUI : MonoBehaviour
{
    public Image icon;
    public Text costText;
    public Image cooldownOverlay;
    
    private PlantData plant;
    private float cooldown;
    
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            cooldownOverlay.fillAmount = cooldown / plant.attackSpeed;
            cooldownOverlay.gameObject.SetActive(true);
        }
        else
        {
            cooldownOverlay.gameObject.SetActive(false);
        }
    }
    
    public void Initialize(PlantData plantData)
    {
        plant = plantData;
        icon.sprite = plant.icon;
        costText.text = plant.cost.ToString();
        cooldown = 0;
    }
    
    public bool CanPlace()
    {
        return cooldown <= 0 && GameManager.Instance.Sunlight >= plant.cost;
    }
    
    public void Place()
    {
        if (CanPlace())
        {
            GameManager.Instance.RemoveSunlight(plant.cost);
            cooldown = 2f;
        }
    }
    
    public PlantData GetPlant()
    {
        return plant;
    }
}
