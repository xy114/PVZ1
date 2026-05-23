using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject levelCardPrefab;
    public Transform levelContainer;
    public Button backButton;
    
    public LevelData[] levels;
    
    void Start()
    {
        backButton.onClick.AddListener(OnBackButton);
        InitializeLevelCards();
    }
    
    void InitializeLevelCards()
    {
        foreach (LevelData level in levels)
        {
            GameObject card = Instantiate(levelCardPrefab, levelContainer);
            LevelCardUI cardUI = card.GetComponent<LevelCardUI>();
            cardUI.Initialize(level);
        }
    }
    
    void OnBackButton()
    {
        SceneLoader.Instance.LoadScene(SceneName.MainMenu);
    }
}

public class LevelCardUI : MonoBehaviour
{
    public Text levelNameText;
    public Text levelNumberText;
    public Text difficultyText;
    public Button selectButton;
    
    private LevelData level;
    
    public void Initialize(LevelData levelData)
    {
        level = levelData;
        levelNameText.text = level.levelName;
        levelNumberText.text = "关卡 " + level.levelNumber;
        
        string difficultyStr = "";
        switch (level.difficulty)
        {
            case 1: difficultyStr = "简单"; break;
            case 2: difficultyStr = "普通"; break;
            case 3: difficultyStr = "困难"; break;
            default: difficultyStr = "普通"; break;
        }
        difficultyText.text = difficultyStr;
        
        selectButton.onClick.AddListener(OnSelectLevel);
    }
    
    void OnSelectLevel()
    {
        GameManager.Instance.SetLevel(level.levelNumber);
        SceneLoader.Instance.LoadScene(SceneName.CharacterSelect);
    }
}
