using System;

namespace Models
{
    public class CastleUpgradeModel
    {
        public int HealthLevel { get; private set; }
        public int AttackSpeedLevel { get; private set; }
        public int DamageLevel { get; private set; }

        private readonly int _healthMaxLevel;
        private readonly int _attackSpeedMaxLevel;
        private readonly int _damageMaxLevel;

        public CastleUpgradeModel(int healthMaxLevel, int attackSpeedMaxLevel, int damageMaxLevel)
        {
            if (healthMaxLevel < 0 || attackSpeedMaxLevel < 0 || damageMaxLevel < 0)
                throw new ArgumentOutOfRangeException("Maximum upgrade level cannot be negative.");

            _healthMaxLevel = healthMaxLevel;
            _attackSpeedMaxLevel = attackSpeedMaxLevel;
            _damageMaxLevel = damageMaxLevel;
        }

        public static int CalculateCost(int baseCost, float costMultiplier, int currentLevel)
        {
            if (baseCost < 0 || costMultiplier < 1f || currentLevel < 0)
                throw new ArgumentOutOfRangeException("Invalid upgrade cost parameters.");

            return (int)Math.Ceiling(baseCost * Math.Pow(costMultiplier, currentLevel));
        }

        public bool CanUpgradeHealth => HealthLevel < _healthMaxLevel;
        public bool CanUpgradeAttackSpeed => AttackSpeedLevel < _attackSpeedMaxLevel;
        public bool CanUpgradeDamage => DamageLevel < _damageMaxLevel;

        public bool UpgradeHealth()
        {
            if (!CanUpgradeHealth) return false;

            HealthLevel++;
            return true;
        }

        public bool UpgradeAttackSpeed()
        {
            if (!CanUpgradeAttackSpeed) return false;

            AttackSpeedLevel++;
            return true;
        }

        public bool UpgradeDamage()
        {
            if (!CanUpgradeDamage) return false;

            DamageLevel++;
            return true;
        }


    }
}