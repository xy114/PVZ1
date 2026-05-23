using UnityEngine;
using UnityEngine.UI;

public class NeonBorderEffect : MonoBehaviour
{
    [Header("Settings")]
    public Color neonColor = Color.cyan;
    public float borderWidth = 2f;
    public float glowIntensity = 1.5f;
    public bool animatePulse = true;
    public float pulseSpeed = 2f;

    private Image borderImage;
    private float time;

    private void Awake()
    {
        CreateBorder();
    }

    private void CreateBorder()
    {
        GameObject borderObj = new GameObject("NeonBorder");
        borderObj.transform.SetParent(transform, false);
        borderObj.transform.SetAsFirstSibling();
        
        borderImage = borderObj.AddComponent<Image>();
        borderImage.sprite = null;
        borderImage.color = neonColor;
        borderImage.type = Image.Type.Sliced;
        borderImage.fillCenter = false;

        RectTransform rect = borderObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private void Update()
    {
        if (animatePulse && borderImage != null)
        {
            time += Time.deltaTime;
            float pulse = 0.5f + Mathf.Sin(time * pulseSpeed) * 0.5f;
            Color color = neonColor;
            color.a = 0.7f + pulse * 0.3f;
            borderImage.color = color;
        }
    }

    public void SetColor(Color color)
    {
        neonColor = color;
        if (borderImage != null)
        {
            borderImage.color = color;
        }
    }
}
