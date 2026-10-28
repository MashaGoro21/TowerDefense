using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private CastleConfig config;
    [SerializeField] private GameObject shootEffect;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject castleHitEffect;

    private bool isDestroyed;
    private float attackTimer;
    private Enemy currentTarget;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float AttackInterval { get; private set; }
    public float AttackDamage { get; private set; }
    
    void Start()
    {
        if (config == null) return;

        MaxHealth = config.health;
        CurrentHealth = MaxHealth;
        AttackInterval = config.attackInterval;
        AttackDamage = config.attackDamage;
    }

    void Update()
    {
        if (isDestroyed) return;

        FindNearestEnemy();

        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f)
        {
            AttackEnemy();
            attackTimer = AttackInterval;
        }
    }

    private void FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        currentTarget = null;
        float nearestDistance = config.attackRange;

        foreach(Enemy enemy in enemies)
        {
            if (enemy.IsDead) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance <= nearestDistance)
            {
                nearestDistance = distance;
                currentTarget = enemy;
            }
        }
    }

    private void AttackEnemy()
    {
        if (currentTarget == null || currentTarget.IsDead) return;

        if (shootEffect != null)
        {
            GameObject effect = Instantiate(shootEffect, firePoint.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        currentTarget.TakeDamage(AttackDamage);
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        CurrentHealth = CurrentHealth - damage;

        if (castleHitEffect != null)
        {
            GameObject effect = Instantiate(castleHitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        if (CurrentHealth <= 0f)
        {
            isDestroyed = true;
            if (GameOverManager.Instance != null)
                GameOverManager.Instance.ShowGameOver();
        }
    }

    public void UpgradeHealth(float amount)
    {
        MaxHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void UpgradeAttackSpeed(float amount)
    {
        AttackInterval = Mathf.Max(0.1f, AttackInterval - amount);
    }

    public void UpgradeDamage(float amount)
    {
        AttackDamage += amount;
    }
}
