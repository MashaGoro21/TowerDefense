using UnityEngine;

namespace Towers
{
    public class TowerAttack : MonoBehaviour
    {
        [SerializeField] private Config.TowerConfig _config;
        [SerializeField] private GameObject _shootEffect;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _shootEffectLifetime = 2f;

        private float _attackTimer;
        private Enemies.Enemy _currentTarget;
        private Enemies.EnemyRegistry _enemyRegistry;

        private void Update()
        {
            if (_config == null) return;

            FindNearestEnemy();

            if (_currentTarget == null) return;

            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0f)
            {
                AttackEnemy();
                _attackTimer = _config.AttackInterval;
            }
        }

        public void SetConfig(Config.TowerConfig newConfig)
        {
            _config = newConfig;
            _attackTimer = 0f;
        }

        public void SetEnemyRegistry(Enemies.EnemyRegistry registry)
        {
            _enemyRegistry = registry;
        }

        private void FindNearestEnemy()
        {
            _currentTarget = null;

            if (_enemyRegistry == null) return;

            float nearestDistance = _config.AttackRange;

            foreach (Enemies.Enemy enemy in _enemyRegistry.Enemies)
            {
                if (enemy == null || enemy.IsDead) continue;

                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= nearestDistance)
                {
                    nearestDistance = distance;
                    _currentTarget = enemy;
                }
            }
        }

        private void AttackEnemy()
        {
            if (_currentTarget == null || _currentTarget.IsDead) return;

            if (_shootEffect != null)
            {
                GameObject effect = Instantiate(_shootEffect, _firePoint.position, Quaternion.identity);
                Destroy(effect, _shootEffectLifetime);
            }

            _currentTarget.TakeDamage(_config.AttackDamage);
        }
    }
}