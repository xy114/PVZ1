using UnityEngine;

[CreateAssetMenu(fileName = "ThemeConfig", menuName = "PVZ_Arknights/ThemeConfig")]
public class ThemeConfig : ScriptableObject
{
    [Header("Colors")]
    public Color MainBackground = new Color(0.082f, 0.082f, 0.098f);
    public Color SecondaryBackground = new Color(0.118f, 0.118f, 0.149f);
    public Color NeonBlue = new Color(0f, 0.761f, 1f);
    public Color NeonCyan = new Color(0f, 1f, 0.82f);
    public Color NeonPurple = new Color(0.616f, 0.306f, 0.867f);
    public Color NeonRed = new Color(1f, 0.278f, 0.341f);
    public Color AccentGold = new Color(1f, 0.843f, 0f);
    public Color TextColor = new Color(0.878f, 0.878f, 0.878f);
    public Color BorderColor = new Color(0.165f, 0.165f, 0.208f);

    [Header("UI Settings")]
    public float NeonGlowIntensity = 1.5f;
    public float AnimationDuration = 0.3f;
}
