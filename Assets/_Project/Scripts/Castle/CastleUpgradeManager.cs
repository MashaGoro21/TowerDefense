using UnityEngine;

namespace Castle
{
    public class CastleUpgradeManager : MonoBehaviour
    {
        public int HealthLevel { get; private set; }
        public int AttackSpeedLevel { get; private set; }
        public int DamageLevel { get; private set; }

        [SerializeField] private Config.CastleConfig _config;
        [SerializeField] private Castle _castle;
        [SerializeField] private Economy.Bank _bank;

        private CastleHealth _castleHealth;

        private void Start()
        {
            _castleHealth = _castle.GetComponent<CastleHealth>();
        }

        public bool CanUpgradeHealth =>
            HealthLevel < _config.HealthUpgrade.MaxLevel;

        public bool CanUpgradeAttackSpeed =>
            AttackSpeedLevel < _config.AttackSpeedUpgrade.MaxLevel;

        public bool CanUpgradeDamage =>
            DamageLevel < _config.DamageUpgrade.MaxLevel;

        public int HealthCost =>
            CanUpgradeHealth ? _config.HealthUpgrade.GetCost(HealthLevel) : 0;

        public int AttackSpeedCost =>
            CanUpgradeAttackSpeed ? _config.AttackSpeedUpgrade.GetCost(AttackSpeedLevel) : 0;

        public int DamageCost =>
            CanUpgradeDamage ? _config.DamageUpgrade.GetCost(DamageLevel) : 0;

        public bool UpgradeHealth()
        {
            if (!CanUpgradeHealth) return false;
            if (!_bank.SpendMoney(HealthCost)) return false;

            _castleHealth.UpgradeHealth(_config.HealthUpgrade.ValuePerLevel);
            HealthLevel++;
            return true;
        }

        public bool UpgradeAttackSpeed()
        {
            if (!CanUpgradeAttackSpeed) return false;
            if (!_bank.SpendMoney(AttackSpeedCost)) return false;

            _castle.UpgradeAttackSpeed(_config.AttackSpeedUpgrade.ValuePerLevel);
            AttackSpeedLevel++;
            return true;
        }

        public bool UpgradeDamage()
        {
            if (!CanUpgradeDamage) return false;
            if (!_bank.SpendMoney(DamageCost)) return false;

            _castle.UpgradeDamage(_config.DamageUpgrade.ValuePerLevel);
            DamageLevel++;
            return true;
        }
    }
}
