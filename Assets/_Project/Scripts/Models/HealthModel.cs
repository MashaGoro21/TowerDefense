using System;

namespace Models
{
    public class HealthModel
    {
        public event Action OnHealthChanged;
        public event Action OnDied;

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public bool IsDead => CurrentHealth <= 0f;

        public HealthModel(float maxHealth)
        {
            if (maxHealth <= 0f)
                throw new ArgumentOutOfRangeException(nameof(maxHealth));

            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead || damage <= 0f) return;

            CurrentHealth = MathF.Max(0f, CurrentHealth - damage);

            OnHealthChanged?.Invoke();
            if (IsDead) OnDied?.Invoke();
        }

        public void IncreaseMaxHealth(float amount)
        {
            if (IsDead || amount <= 0f) return;

            MaxHealth += amount;
            CurrentHealth = MathF.Min(CurrentHealth + amount, MaxHealth);

            OnHealthChanged?.Invoke();
        }
    }
}
