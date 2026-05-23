using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantSkill", menuName = "PVZ_Arknights/PlantSkill")]
public class PlantSkill : ScriptableObject
{
    public string skillName;
    public string description;
    public float cooldown;
    public float skillDuration;
    public SkillType skillType;
    public int damage;
    public float range;
    public float effectRadius;
    public SkillEffect effect;
}

public enum SkillType
{
    Active,
    Passive,
    Ultimate
}

public enum SkillEffect
{
    Damage,
    Heal,
    Slow,
    Stun,
    Shield,
    Buff,
    Debuff
}
