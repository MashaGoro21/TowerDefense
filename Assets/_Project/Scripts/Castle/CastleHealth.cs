using UnityEngine;

namespace Castle
{
    public class CastleHealth : MonoBehaviour
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public bool IsDestroyed { get; private set; }
        
        [SerializeField] private GameObject _castleHitEffect;
        [SerializeField] private UI.GameOverManager _gameOverManager;
        [SerializeField] private float _castleHitEffectLifetime = 2f;
        [SerializeField] private Config.CastleConfig _config;

        private void Start()
        {
            if (_config == null) return;

            MaxHealth = _config.Health;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (IsDestroyed) return;

            CurrentHealth = CurrentHealth - damage;

            if (_castleHitEffect != null)
            {
                GameObject effect = Instantiate(_castleHitEffect, transform.position, Quaternion.identity);
                Destroy(effect, _castleHitEffectLifetime);
            }

            if (CurrentHealth <= 0f)
            {
                IsDestroyed = true;
                if (_gameOverManager != null)
                    _gameOverManager.ShowGameOver();
            }
        }

        public void UpgradeHealth(float amount)
        {
            MaxHealth += amount;
            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        }
    }
}