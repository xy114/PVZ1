using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectEnhanced : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Settings")]
    public float scrollSensitivity = 10f;
    public float dragSensitivity = 1f;
    public float smoothness = 5f;
    public bool useInertia = true;
    public float decelerationRate = 0.135f;

    private ScrollRect scrollRect;
    private RectTransform content;
    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private Vector2 contentStartPosition;
    private Vector2 velocity;
    private Vector2 lastContentPosition;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        scrollRect.scrollSensitivity = scrollSensitivity;
        scrollRect.inertia = useInertia;
        scrollRect.decelerationRate = decelerationRate;
    }

    private void Update()
    {
        if (content == null) return;

        HandleMouseScroll();
        HandleDragScroll();

        if (!isDragging && useInertia && velocity.magnitude > 0.1f)
        {
            content.anchoredPosition += velocity * Time.unscaledDeltaTime;
            velocity *= (1f - decelerationRate * Time.unscaledDeltaTime);
        }
    }

    private void HandleMouseScroll()
    {
        if (isDragging) return;

        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            Vector2 newPosition = content.anchoredPosition;
            if (scrollRect.vertical)
                newPosition.y += scrollDelta * scrollSensitivity * 50f;
            if (scrollRect.horizontal)
                newPosition.x -= scrollDelta * scrollSensitivity * 50f;
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, newPosition, smoothness * Time.unscaledDeltaTime);
        }
    }

    private void HandleDragScroll()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        dragStartPosition = eventData.position;
        contentStartPosition = content.anchoredPosition;
        velocity = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        if (useInertia)
        {
            velocity = (content.anchoredPosition - lastContentPosition) / Time.unscaledDeltaTime;
        }
    }

    private void LateUpdate()
    {
        if (isDragging)
        {
            Vector2 currentPos = Input.mousePosition;
            Vector2 delta = (Vector2)currentPos - dragStartPosition;
            
            Vector2 newPosition = contentStartPosition;
            if (scrollRect.vertical)
                newPosition.y += delta.y * dragSensitivity;
            if (scrollRect.horizontal)
                newPosition.x += delta.x * dragSensitivity;
            
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, newPosition, smoothness * Time.unscaledDeltaTime);
            lastContentPosition = content.anchoredPosition;
        }
    }
}
