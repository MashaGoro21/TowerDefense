using Models;
using UnityEngine;

namespace Castle
{
    public class CastleUpgradeManager : MonoBehaviour
    {
        public int HealthLevel => _model.HealthLevel;
        public int AttackSpeedLevel => _model.AttackSpeedLevel;
        public int DamageLevel => _model.DamageLevel;
        public bool CanUpgradeHealth => _model.CanUpgradeHealth;
        public bool CanUpgradeAttackSpeed => _model.CanUpgradeAttackSpeed;
        public bool CanUpgradeDamage => _model.CanUpgradeDamage;
        public int HealthCost => CanUpgradeHealth
            ? CastleUpgradeModel.CalculateCost(
                _config.HealthUpgrade.BaseCost,
                _config.HealthUpgrade.CostMultiplier,
                HealthLevel)
            : 0;
        public int AttackSpeedCost => CanUpgradeAttackSpeed
            ? CastleUpgradeModel.CalculateCost(
                _config.AttackSpeedUpgrade.BaseCost,
                _config.AttackSpeedUpgrade.CostMultiplier,
                AttackSpeedLevel)
            : 0;
        public int DamageCost => CanUpgradeDamage
            ? CastleUpgradeModel.CalculateCost(
                _config.DamageUpgrade.BaseCost,
                _config.DamageUpgrade.CostMultiplier,
                DamageLevel)
            : 0;

        [SerializeField] private Config.CastleConfig _config;
        [SerializeField] private Castle _castle;
        [SerializeField] private Economy.Bank _bank;

        private CastleHealth _castleHealth;
        private CastleUpgradeModel _model;

        private void Awake()
        {
            _castleHealth = _castle.GetComponent<CastleHealth>();
            _model = new CastleUpgradeModel(
                _config.HealthUpgrade.MaxLevel, 
                _config.AttackSpeedUpgrade.MaxLevel, 
                _config.DamageUpgrade.MaxLevel);
        }

        public bool UpgradeHealth()
        {
            if (!CanUpgradeHealth || !_bank.CanAfford(HealthCost)) return false;

            _bank.SpendMoney(HealthCost);
            _model.UpgradeHealth();
            _castleHealth.UpgradeHealth(_config.HealthUpgrade.ValuePerLevel);
            return true;
        }

        public bool UpgradeAttackSpeed()
        {
            if (!CanUpgradeAttackSpeed || !_bank.CanAfford(AttackSpeedCost)) return false;

            _bank.SpendMoney(AttackSpeedCost);
            _model.UpgradeAttackSpeed();
            _castle.UpgradeAttackSpeed(_config.AttackSpeedUpgrade.ValuePerLevel);
            return true;
        }

        public bool UpgradeDamage()
        {
            if (!CanUpgradeDamage || !_bank.CanAfford(DamageCost)) return false;

            _bank.SpendMoney(DamageCost);
            _model.UpgradeDamage();
            _castle.UpgradeDamage(_config.DamageUpgrade.ValuePerLevel);
            return true;
        }
    }
}
