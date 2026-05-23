using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantSkin", menuName = "PVZ_Arknights/PlantSkin")]
public class PlantSkin : ScriptableObject
{
    public string skinName;
    public string arknightsCharacter;
    public Sprite skinSprite;
    public Color primaryColor;
    public Color secondaryColor;
    public float healthMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;
    public PlantSkill skill;
}
