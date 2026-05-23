using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("References")]
    public ThemeConfig theme;

    [Header("Audio")]
    public AudioClip buttonClickSound;

    private void Awake()
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

        if (theme == null)
        {
            Debug.LogWarning("UIManager: No ThemeConfig assigned!");
        }
    }

    public void PlayButtonClick()
    {
        if (buttonClickSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySoundEffect(buttonClickSound);
        }
    }

    public void ApplyTheme(GameObject uiElement)
    {
        if (theme == null) return;

        Image image = uiElement.GetComponent<Image>();
        if (image != null && uiElement.name.Contains("Background"))
        {
            image.color = theme.SecondaryBackground;
        }

        Text text = uiElement.GetComponentInChildren<Text>();
        if (text != null)
        {
            text.color = theme.TextColor;
        }
    }

    public IEnumerator FadeIn(CanvasGroup group, float duration = 0.3f)
    {
        float time = 0f;
        while (time < duration)
        {
            group.alpha = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = 1f;
    }

    public IEnumerator FadeOut(CanvasGroup group, float duration = 0.3f)
    {
        float time = 0f;
        while (time < duration)
        {
            group.alpha = Mathf.Lerp(1f, 0f, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = 0f;
    }
}
