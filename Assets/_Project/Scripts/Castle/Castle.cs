using UnityEngine;
using Models;

namespace Castle
{
    [RequireComponent(typeof(CastleHealth))]
    public class Castle : MonoBehaviour
    {
        public float AttackInterval => _attackModel.AttackInterval;
        public float AttackDamage => _attackModel.AttackDamage;
        
        [SerializeField] private Config.CastleConfig _config;
        [SerializeField] private GameObject _shootEffect;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Enemies.EnemyRegistry _enemyRegistry;
        [SerializeField] private float _minAttackInterval = 0.1f;
        [SerializeField] private float _shootEffectLifetime = 2f;

        private float _attackTimer;
        private Enemies.Enemy _currentTarget;
        private CastleHealth _castleHealth;
        private CastleAttackModel _attackModel;

        private void Awake()
        {
            _castleHealth = GetComponent<CastleHealth>();
            _attackModel = new CastleAttackModel(_config.AttackDamage, _config.AttackInterval, _minAttackInterval);
        }

        private void Update()
        {
            if (_castleHealth == null || _castleHealth.IsDestroyed) return;

            FindNearestEnemy();

            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0f)
            {
                AttackEnemy();
                _attackTimer = AttackInterval;
            }
        }

        public void UpgradeAttackSpeed(float amount)
        {
            _attackModel.UpgradeAttackSpeed(amount);
        }

        public void UpgradeDamage(float amount)
        {
            _attackModel.UpgradeDamage(amount);
        }

        private void FindNearestEnemy()
        {
            _currentTarget = null;

            if (_enemyRegistry == null) return;

            float nearestDistance = _config.AttackRange;

            foreach (Enemies.Enemy enemy in _enemyRegistry.Enemies)
            {
                if (enemy.IsDead) continue;

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

            _currentTarget.TakeDamage(AttackDamage);
        }
    }
}