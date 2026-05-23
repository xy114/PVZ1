using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Button levelSelectButton;
    public Button characterSelectButton;
    public Button settingsButton;
    
    void Start()
    {
        startButton.onClick.AddListener(OnStartButton);
        levelSelectButton.onClick.AddListener(OnLevelSelectButton);
        characterSelectButton.onClick.AddListener(OnCharacterSelectButton);
        settingsButton.onClick.AddListener(OnSettingsButton);
        
        GameManager.Instance.SetGameState(GameState.Menu);
    }
    
    void OnStartButton()
    {
        SceneLoader.Instance.LoadScene(SceneName.LevelSelect);
    }
    
    void OnLevelSelectButton()
    {
        SceneLoader.Instance.LoadScene(SceneName.LevelSelect);
    }
    
    void OnCharacterSelectButton()
    {
        SceneLoader.Instance.LoadScene(SceneName.CharacterSelect);
    }
    
    void OnSettingsButton()
    {
        
    }
}
