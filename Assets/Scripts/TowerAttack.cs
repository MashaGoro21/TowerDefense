using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private TowerConfig config;
    [SerializeField] private GameObject shootEffect;
    [SerializeField] private Transform firePoint;

    private float attackTimer;
    private Enemy currentTarget;


    void Update()
    {
        if (config == null) return;

        FindNearestEnemy();

        if (currentTarget == null) return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            AttackEnemy();
            attackTimer = config.attackInterval;
        }
    }

    private void FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        currentTarget = null;
        float nearestDistance = config.attackRange;

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null || enemy.IsDead) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= nearestDistance)
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

        currentTarget.TakeDamage(config.attackDamage);
    }

    public void SetConfig(TowerConfig newConfig)
    {
        config = newConfig;
        attackTimer = 0f;
    }
}
