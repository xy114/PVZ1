using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData data;
    
    private int currentHealth;
    private float moveSpeed;
    private float attackTimer;
    private bool isStunned;
    private float stunTimer;
    private bool isSlowed;
    private float slowTimer;
    private float slowMultiplier;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    public void Initialize(ZombieData zombieData)
    {
        data = zombieData;
        currentHealth = data.health;
        moveSpeed = data.speed;
        spriteRenderer.sprite = data.sprite;
    }
    
    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
            }
            return;
        }
        
        if (isSlowed)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                isSlowed = false;
                slowMultiplier = 1f;
            }
        }
        
        Move();
        Attack();
    }
    
    void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * slowMultiplier * Time.deltaTime);
        
        if (transform.position.x < -15f)
        {
            GameManager.Instance.SetGameState(GameState.GameOver);
            Destroy(gameObject);
        }
    }
    
    void Attack()
    {
        attackTimer += Time.deltaTime;
        
        if (attackTimer >= 1f / data.attackSpeed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask("Plant"));
            
            if (hit.collider != null)
            {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    plant.TakeDamage(data.damage);
                }
            }
            attackTimer = 0;
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        EventManager.Instance.Trigger(GameEvents.ZOMBIE_KILLED, this);
        Destroy(gameObject);
    }
    
    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
    }
    
    public void Slow(float duration)
    {
        isSlowed = true;
        slowTimer = duration;
        slowMultiplier = 0.5f;
    }
    
    public float GetHealthPercentage()
    {
        return (float)currentHealth / data.health;
    }
}
