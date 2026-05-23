using UnityEngine;

[CreateAssetMenu(fileName = "NewPlant", menuName = "PVZ_Arknights/Plant")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public Sprite icon;
    public Sprite defaultSprite;
    public int cost;
    public int health;
    public int damage;
    public float attackSpeed;
    public float range;
    public PlantType plantType;
    public PlantSkin[] availableSkins;
    
    public PlantSkin GetDefaultSkin()
    {
        if (availableSkins != null && availableSkins.Length > 0)
        {
            return availableSkins[0];
        }
        return null;
    }
}

public enum PlantType
{
    Attack,
    Defense,
    Support,
    Production
}
