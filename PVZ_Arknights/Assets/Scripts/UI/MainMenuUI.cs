using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button levelSelectButton;
    public Button characterSelectButton;
    public Button settingsButton;

    [Header("Animations")]
    public CanvasGroup menuCanvas;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButton);
        levelSelectButton.onClick.AddListener(OnLevelSelectButton);
        characterSelectButton.onClick.AddListener(OnCharacterSelectButton);
        settingsButton.onClick.AddListener(OnSettingsButton);

        AddNeonEffects();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameState(GameState.Menu);
        }

        if (menuCanvas != null)
        {
            StartCoroutine(UIManager.Instance.FadeIn(menuCanvas, 0.5f));
        }
    }

    private void AddNeonEffects()
    {
        AddNeonToButton(startButton);
        AddNeonToButton(levelSelectButton);
        AddNeonToButton(characterSelectButton);
        AddNeonToButton(settingsButton);
    }

    private void AddNeonToButton(Button button)
    {
        if (button != null && button.gameObject.GetComponent<NeonBorderEffect>() == null)
        {
            NeonBorderEffect neon = button.gameObject.AddComponent<NeonBorderEffect>();
            if (UIManager.Instance != null && UIManager.Instance.theme != null)
            {
                neon.neonColor = UIManager.Instance.theme.NeonBlue;
            }
        }
    }

    private void OnStartButton()
    {
        PlayClickSound();
        SceneLoader.Instance.LoadScene(SceneName.LevelSelect);
    }

    private void OnLevelSelectButton()
    {
        PlayClickSound();
        SceneLoader.Instance.LoadScene(SceneName.LevelSelect);
    }

    private void OnCharacterSelectButton()
    {
        PlayClickSound();
        SceneLoader.Instance.LoadScene(SceneName.CharacterSelect);
    }

    private void OnSettingsButton()
    {
        PlayClickSound();
        // TODO: Implement settings
    }

    private void PlayClickSound()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.PlayButtonClick();
        }
    }
}
