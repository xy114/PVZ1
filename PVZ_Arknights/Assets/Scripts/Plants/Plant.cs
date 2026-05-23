using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantData data;
    public PlantSkin currentSkin;
    
    private int currentHealth;
    private float attackTimer;
    private float skillCooldown;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    public void Initialize(PlantData plantData, PlantSkin skin = null)
    {
        data = plantData;
        currentSkin = skin ?? plantData.GetDefaultSkin();
        
        ApplySkin();
        currentHealth = Mathf.RoundToInt(data.health * currentSkin.healthMultiplier);
        
        if (data.plantType == PlantType.Production)
        {
            InvokeRepeating("ProduceSunlight", 7.5f, 7.5f);
        }
    }
    
    void ApplySkin()
    {
        if (currentSkin != null)
        {
            spriteRenderer.sprite = currentSkin.skinSprite ?? data.defaultSprite;
            spriteRenderer.color = currentSkin.primaryColor;
        }
        else
        {
            spriteRenderer.sprite = data.defaultSprite;
        }
    }
    
    void Update()
    {
        if (data.plantType == PlantType.Attack)
        {
            attackTimer += Time.deltaTime;
            float attackInterval = 1f / (data.attackSpeed * currentSkin.attackSpeedMultiplier);
            
            if (attackTimer >= attackInterval)
            {
                Attack();
                attackTimer = 0;
            }
        }
        
        if (skillCooldown > 0)
        {
            skillCooldown -= Time.deltaTime;
        }
    }
    
    void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, data.range, LayerMask.GetMask("Zombie"));
        
        if (hit.collider != null)
        {
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(Mathf.RoundToInt(data.damage * currentSkin.damageMultiplier));
            }
        }
    }
    
    void ProduceSunlight()
    {
        GameObject sunlight = Instantiate(Resources.Load<GameObject>("Prefabs/Sunlight/Sunlight"));
        sunlight.transform.position = transform.position + Vector3.up;
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
        Destroy(gameObject);
    }
    
    public void UseSkill()
    {
        if (skillCooldown <= 0 && currentSkin != null && currentSkin.skill != null)
        {
            skillCooldown = currentSkin.skill.cooldown;
            
            switch (currentSkin.skill.effect)
            {
                case SkillEffect.Damage:
                    DealAreaDamage();
                    break;
                case SkillEffect.Slow:
                    SlowEnemies();
                    break;
                case SkillEffect.Stun:
                    StunEnemies();
                    break;
                case SkillEffect.Heal:
                    HealNearbyPlants();
                    break;
            }
        }
    }
    
    void DealAreaDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, currentSkin.skill.effectRadius, LayerMask.GetMask("Zombie"));
        
        foreach (Collider2D collider in hitColliders)
        {
            Zombie zombie = collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(currentSkin.skill.damage);
            }
        }
    }
    
    void SlowEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, currentSkin.skill.effectRadius, LayerMask.GetMask("Zombie"));
        
        foreach (Collider2D collider in hitColliders)
        {
            Zombie zombie = collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.Slow(currentSkin.skill.skillDuration);
            }
        }
    }
    
    void StunEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, currentSkin.skill.effectRadius, LayerMask.GetMask("Zombie"));
        
        foreach (Collider2D collider in hitColliders)
        {
            Zombie zombie = collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.Stun(currentSkin.skill.skillDuration);
            }
        }
    }
    
    void HealNearbyPlants()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, currentSkin.skill.effectRadius, LayerMask.GetMask("Plant"));
        
        foreach (Collider2D collider in hitColliders)
        {
            Plant plant = collider.GetComponent<Plant>();
            if (plant != null)
            {
                plant.Heal(currentSkin.skill.damage);
            }
        }
    }
    
    void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, Mathf.RoundToInt(data.health * currentSkin.healthMultiplier));
    }
    
    public float GetHealthPercentage()
    {
        return (float)currentHealth / (data.health * currentSkin.healthMultiplier);
    }
}
