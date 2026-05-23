using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    public LevelData currentLevel;
    public int currentPhaseIndex;
    public int currentWave;
    public int totalWaves;
    
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
    
    public void StartLevel(LevelData level)
    {
        currentLevel = level;
        currentPhaseIndex = 0;
        currentWave = 0;
        totalWaves = 0;
        
        foreach (LevelPhase phase in level.phases)
        {
            totalWaves += phase.waves;
        }
        
        StartPhase(currentLevel.phases[currentPhaseIndex]);
    }
    
    void StartPhase(LevelPhase phase)
    {
        EventManager.Instance.Trigger(GameEvents.PHASE_CHANGED, phase.phaseType);
        WaveManager.Instance.StartPhase(phase);
    }
    
    public void OnWaveComplete()
    {
        currentWave++;
        
        LevelPhase currentPhase = currentLevel.phases[currentPhaseIndex];
        
        if (currentWave % currentPhase.waves == 0)
        {
            currentPhaseIndex++;
            
            if (currentPhaseIndex >= currentLevel.phases.Length)
            {
                LevelComplete();
            }
            else
            {
                StartPhase(currentLevel.phases[currentPhaseIndex]);
            }
        }
        
        EventManager.Instance.Trigger(GameEvents.WAVE_COMPLETE, currentWave, totalWaves);
    }
    
    void LevelComplete()
    {
        GameManager.Instance.SetGameState(GameState.Victory);
        EventManager.Instance.Trigger(GameEvents.LEVEL_COMPLETE);
    }
    
    public PhaseType GetCurrentPhaseType()
    {
        if (currentPhaseIndex < currentLevel.phases.Length)
        {
            return currentLevel.phases[currentPhaseIndex].phaseType;
        }
        return PhaseType.Early;
    }
}
