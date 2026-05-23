using UnityEngine;

public class Sunlight : MonoBehaviour
{
    public int value = 50;
    public float fadeDelay = 3f;
    public float fadeDuration = 3f;
    
    private bool isOnGround;
    private float groundTimer;
    private float fadeTimer;
    private SpriteRenderer spriteRenderer;
    private bool isFading;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (!isOnGround)
        {
            transform.Translate(Vector2.down * 2f * Time.deltaTime);
        }
        else
        {
            groundTimer += Time.deltaTime;
            
            if (groundTimer >= fadeDelay && !isFading)
            {
                isFading = true;
                fadeTimer = fadeDuration;
            }
            
            if (isFading)
            {
                fadeTimer -= Time.deltaTime;
                float alpha = fadeTimer / fadeDuration;
                spriteRenderer.color = new Color(1f, 1f, 0f, alpha);
                
                if (fadeTimer <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
    
    void OnMouseDown()
    {
        Collect();
    }
    
    public void Collect()
    {
        GameManager.Instance.AddSunlight(value);
        EventManager.Instance.Trigger(GameEvents.SUNLIGHT_COLLECTED, value);
        Destroy(gameObject);
    }
}
