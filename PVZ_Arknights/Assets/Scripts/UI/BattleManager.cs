using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    
    public Grid grid;
    public GameObject plantPrefab;
    public LayerMask plantLayer;
    
    private PlantData selectedPlant;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        HandleInput();
    }
    
    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, plantLayer);
            
            if (hit.collider != null)
            {
                PlantSlotUI slot = hit.collider.GetComponent<PlantSlotUI>();
                if (slot != null && slot.CanPlace())
                {
                    selectedPlant = slot.GetPlant();
                    slot.Place();
                }
            }
            else if (selectedPlant != null)
            {
                PlacePlant(mousePosition);
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            selectedPlant = null;
        }
    }
    
    void PlacePlant(Vector2 position)
    {
        Vector3 worldPosition = new Vector3(position.x, position.y, 0);
        Vector3Int gridPosition = grid.WorldToCell(worldPosition);
        Vector3 cellCenter = grid.GetCellCenterWorld(gridPosition);
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(cellCenter, 0.4f, LayerMask.GetMask("Plant"));
        
        if (hitColliders.Length == 0)
        {
            GameObject plantObject = Instantiate(plantPrefab, cellCenter, Quaternion.identity);
            Plant plant = plantObject.GetComponent<Plant>();
            plant.Initialize(selectedPlant);
            
            EventManager.Instance.Trigger(GameEvents.PLANT_PLACED, plant);
        }
        
        selectedPlant = null;
    }
    
    public void SelectPlant(PlantData plant)
    {
        selectedPlant = plant;
    }
}
