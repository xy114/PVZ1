using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "PVZ_Arknights/Level")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int levelNumber;
    public int difficulty;
    public LevelPhase[] phases;
    public Sprite backgroundSprite;
}

[System.Serializable]
public class LevelPhase
{
    public PhaseType phaseType;
    public int waves;
    public float spawnInterval;
    public int zombiesPerWave;
    public ZombieData[] zombieTypes;
}

public enum PhaseType
{
    Early,
    Mid,
    Late
}
