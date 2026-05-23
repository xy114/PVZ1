using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterSelectUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject plantCardPrefab;
    public Transform plantContainer;
    public Button startButton;
    public Button backButton;
    public Text selectedCountText;
    
    public PlantData[] allPlants;
    
    void Start()
    {
        startButton.onClick.AddListener(OnStartButton);
        backButton.onClick.AddListener(OnBackButton);
        InitializePlantCards();
        UpdateSelectedCount();
        
        GameManager.Instance.OnSunlightChanged += UpdateSunlightDisplay;
    }
    
    void InitializePlantCards()
    {
        foreach (PlantData plant in allPlants)
        {
            GameObject card = Instantiate(plantCardPrefab, plantContainer);
            PlantCardUI cardUI = card.GetComponent<PlantCardUI>();
            cardUI.Initialize(plant);
            cardUI.OnSelect += OnPlantSelected;
        }
    }
    
    void OnPlantSelected(PlantData plant, bool selected)
    {
        if (selected)
        {
            GameManager.Instance.SelectPlant(plant);
        }
        else
        {
            GameManager.Instance.DeselectPlant(plant);
        }
        UpdateSelectedCount();
    }
    
    void UpdateSelectedCount()
    {
        selectedCountText.text = $"已选择: {GameManager.Instance.SelectedPlants.Count}/6";
        startButton.interactable = GameManager.Instance.SelectedPlants.Count > 0;
    }
    
    void UpdateSunlightDisplay(int amount)
    {
        
    }
    
    void OnStartButton()
    {
        SceneLoader.Instance.LoadScene(SceneName.Battle);
    }
    
    void OnBackButton()
    {
        GameManager.Instance.ClearSelectedPlants();
        SceneLoader.Instance.LoadScene(SceneName.LevelSelect);
    }
}

public class PlantCardUI : MonoBehaviour
{
    public event System.Action<PlantData, bool> OnSelect;
    
    public Image icon;
    public Text nameText;
    public Text costText;
    public Toggle selectToggle;
    public Dropdown skinDropdown;
    
    private PlantData plant;
    
    public void Initialize(PlantData plantData)
    {
        plant = plantData;
        icon.sprite = plant.icon;
        nameText.text = plant.plantName;
        costText.text = plant.cost.ToString();
        
        if (plant.availableSkins != null && plant.availableSkins.Length > 0)
        {
            skinDropdown.ClearOptions();
            List<string> skinNames = new List<string>();
            foreach (PlantSkin skin in plant.availableSkins)
            {
                skinNames.Add(skin.skinName);
            }
            skinDropdown.AddOptions(skinNames);
        }
        else
        {
            skinDropdown.gameObject.SetActive(false);
        }
        
        selectToggle.onValueChanged.AddListener(OnToggleChanged);
    }
    
    void OnToggleChanged(bool isOn)
    {
        OnSelect?.Invoke(plant, isOn);
    }
    
    public PlantSkin GetSelectedSkin()
    {
        if (plant.availableSkins != null && plant.availableSkins.Length > 0)
        {
            return plant.availableSkins[skinDropdown.value];
        }
        return null;
    }
}
