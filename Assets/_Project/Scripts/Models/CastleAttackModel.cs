using System;

namespace Models
{
    public class CastleAttackModel
    {
        public float AttackDamage { get; private set; }
        public float AttackInterval { get; private set; }

        private readonly float _minAttackInterval;

        public CastleAttackModel(float attackDamage, float attackInterval, float minAttackInterval)
        {
            if (attackDamage < 0f)
                throw new ArgumentOutOfRangeException(nameof(attackDamage));

            if (minAttackInterval <= 0f)
                throw new ArgumentOutOfRangeException(nameof(minAttackInterval));

            if (attackInterval < minAttackInterval)
                throw new ArgumentOutOfRangeException(nameof(attackInterval));

            AttackDamage = attackDamage;
            AttackInterval = attackInterval;
            _minAttackInterval = minAttackInterval;
        }

        public void UpgradeDamage(float amount)
        {
            if (amount <= 0f) return;

            AttackDamage += amount;
        }

        public void UpgradeAttackSpeed(float amount)
        {
            if (amount <= 0f) return;

            AttackInterval = MathF.Max(_minAttackInterval, AttackInterval - amount);
        }
    }
}