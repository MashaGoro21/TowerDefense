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
            ? _model.CalculateCost(
                _config.HealthUpgrade.BaseCost,
                _config.HealthUpgrade.CostMultiplier,
                HealthLevel)
            : 0;
        public int AttackSpeedCost => CanUpgradeAttackSpeed
            ? _model.CalculateCost(
                _config.AttackSpeedUpgrade.BaseCost,
                _config.AttackSpeedUpgrade.CostMultiplier,
                AttackSpeedLevel)
            : 0;
        public int DamageCost => CanUpgradeDamage
            ? _model.CalculateCost(
                _config.DamageUpgrade.BaseCost,
                _config.DamageUpgrade.CostMultiplier,
                DamageLevel)
            : 0;

        [SerializeField] private Config.CastleConfig _config;
        [SerializeField] private Castle _castle;
        [SerializeField] private Economy.Bank _bank;

        private CastleHealth _castleHealth;
        private CastleUpgradeModel _model;
        private bool _isPaused;

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
            if (_isPaused) return false;
            if (!CanUpgradeHealth || !_bank.SpendMoney(HealthCost)) return false;
            
            _model.UpgradeHealth();
            _castleHealth.UpgradeHealth(_config.HealthUpgrade.ValuePerLevel);
            
            return true;
        }

        public bool UpgradeAttackSpeed()
        {
            if (_isPaused) return false;
            if (!CanUpgradeAttackSpeed || !_bank.SpendMoney(AttackSpeedCost)) return false;

            _model.UpgradeAttackSpeed();
            _castle.UpgradeAttackSpeed(_config.AttackSpeedUpgrade.ValuePerLevel);
            
            return true;
        }

        public bool UpgradeDamage()
        {
            if (_isPaused) return false;
            if (!CanUpgradeDamage || !_bank.SpendMoney(DamageCost)) return false;

            _model.UpgradeDamage();
            _castle.UpgradeDamage(_config.DamageUpgrade.ValuePerLevel);
            
            return true;
        }

        public void StopUpgrades()
        {
            _isPaused = true;
        }
    }
}
