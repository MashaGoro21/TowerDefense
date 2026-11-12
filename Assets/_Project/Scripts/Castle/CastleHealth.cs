using Models;
using System;
using UnityEngine;

namespace Castle
{
    public class CastleHealth : MonoBehaviour
    {
        public event Action OnHealthChanged;
        public event Action OnDestroyed;

        public float MaxHealth => _healthModel.MaxHealth;
        public float CurrentHealth => _healthModel.CurrentHealth;
        public bool IsDestroyed => _healthModel.IsDead;
        
        [SerializeField] private GameObject _castleHitEffect;
        [SerializeField] private float _castleHitEffectLifetime = 2f;
        [SerializeField] private Config.CastleConfig _config;

        private HealthModel _healthModel;

        private void Awake()
        {
            _healthModel = new HealthModel(_config.Health);

            _healthModel.OnHealthChanged += HandleHealthChanged;
            _healthModel.OnDied += HandleDestroyed;

        }

        private void OnDestroy()
        {
            _healthModel.OnHealthChanged -= HandleHealthChanged;
            _healthModel.OnDied -= HandleDestroyed;
        }

        public void TakeDamage(float damage)
        {
            if (IsDestroyed || damage <= 0f) return;

            _healthModel.TakeDamage(damage);

            if (_castleHitEffect != null)
            {
                GameObject effect = Instantiate(_castleHitEffect, transform.position, Quaternion.identity);
                Destroy(effect, _castleHitEffectLifetime);
            }
        }

        public void UpgradeHealth(float amount)
        {
            _healthModel.IncreaseMaxHealth(amount);
        }

        private void HandleHealthChanged()
        {
            OnHealthChanged?.Invoke();
        }

        private void HandleDestroyed()
        {
            OnDestroyed?.Invoke();
        }
    }
}