using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectUI : MonoBehaviour
{
    [Header("UI References")]
    public ScrollRectEnhanced scrollRect;
    public GameObject levelCardPrefab;
    public Transform levelContainer;
    public Button backButton;

    [Header("Level Data")]
    public LevelDataSO[] levels;

    private List<GameObject> levelCards = new List<GameObject>();

    private void Start()
    {
        backButton.onClick.AddListener(OnBackButton);
        InitializeLevelCards();
        
        AddNeonEffects();
    }

    private void InitializeLevelCards()
    {
        foreach (Transform child in levelContainer)
        {
            Destroy(child.gameObject);
        }
        levelCards.Clear();

        foreach (LevelDataSO level in levels)
        {
            GameObject card = Instantiate(levelCardPrefab, levelContainer);
            LevelCardUI cardUI = card.GetComponent<LevelCardUI>();
            if (cardUI != null)
            {
                cardUI.Initialize(level);
            }
            levelCards.Add(card);
        }
    }

    private void AddNeonEffects()
    {
        AddNeonToButton(backButton);
    }

    private void AddNeonToButton(Button button)
    {
        if (button != null && button.gameObject.GetComponent<NeonBorderEffect>() == null)
        {
            NeonBorderEffect neon = button.gameObject.AddComponent<NeonBorderEffect>();
            if (UIManager.Instance != null && UIManager.Instance.theme != null)
            {
                neon.neonColor = UIManager.Instance.theme.NeonCyan;
            }
        }
    }

    private void OnBackButton()
    {
        PlayClickSound();
        SceneLoader.Instance.LoadScene(SceneName.MainMenu);
    }

    private void PlayClickSound()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.PlayButtonClick();
        }
    }
}
