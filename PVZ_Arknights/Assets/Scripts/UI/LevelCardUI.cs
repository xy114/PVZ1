using UnityEngine;
using UnityEngine.UI;

public class LevelCardUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text levelNameText;
    public Text levelNumberText;
    public Text difficultyText;
    public Image difficultyBackground;
    public Button selectButton;

    private LevelDataSO levelData;

    public void Initialize(LevelDataSO level)
    {
        levelData = level;
        
        if (levelNameText != null)
            levelNameText.text = level.levelName;
        
        if (levelNumberText != null)
            levelNumberText.text = $"关卡 {level.levelNumber}";
        
        if (difficultyText != null)
        {
            string difficultyStr = "";
            Color bgColor = Color.cyan;
            
            switch (level.difficulty)
            {
                case 1:
                    difficultyStr = "简单";
                    bgColor = new Color(0f, 0.761f, 1f);
                    break;
                case 2:
                    difficultyStr = "普通";
                    bgColor = new Color(0f, 1f, 0.82f);
                    break;
                case 3:
                    difficultyStr = "困难";
                    bgColor = new Color(1f, 0.278f, 0.341f);
                    break;
            }
            
            difficultyText.text = difficultyStr;
            
            if (difficultyBackground != null)
                difficultyBackground.color = bgColor;
        }
        
        if (selectButton != null)
        {
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(OnSelectLevel);
        }
    }

    private void OnSelectLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetLevel(levelData.levelNumber);
        }
        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.PlayButtonClick();
        }
        
        SceneLoader.Instance.LoadScene(SceneName.CharacterSelect);
    }
}
