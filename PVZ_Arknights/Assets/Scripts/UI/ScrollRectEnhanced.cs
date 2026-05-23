using UnityEngine;
using UnityEngine.UI;

public class ScrollRectEnhanced : ScrollRect
{
    public float scrollSensitivity = 10f;
    public float dragSensitivity = 1f;
    
    private Vector2 dragStartPosition;
    private Vector2 scrollStartPosition;
    private bool isDragging = false;
    
    protected override void Awake()
    {
        base.Awake();
        movementType = MovementType.Clamped;
    }
    
    void Update()
    {
        HandleMouseScroll();
        HandleMouseDrag();
    }
    
    void HandleMouseScroll()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        
        if (scrollDelta != 0 && content != null)
        {
            Vector2 newPosition = content.anchoredPosition;
            
            if (horizontal)
            {
                newPosition.x += scrollDelta * scrollSensitivity * 50f;
            }
            
            if (vertical)
            {
                newPosition.y -= scrollDelta * scrollSensitivity * 50f;
            }
            
            content.anchoredPosition = newPosition;
        }
    }
    
    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = Input.mousePosition;
            scrollStartPosition = content.anchoredPosition;
            isDragging = true;
        }
        
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentPosition = Input.mousePosition;
            Vector2 delta = currentPosition - dragStartPosition;
            
            Vector2 newPosition = scrollStartPosition;
            
            if (horizontal)
            {
                newPosition.x += delta.x * dragSensitivity;
            }
            
            if (vertical)
            {
                newPosition.y -= delta.y * dragSensitivity;
            }
            
            content.anchoredPosition = newPosition;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
