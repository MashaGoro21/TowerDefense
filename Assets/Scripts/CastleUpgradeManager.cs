using UnityEngine;

public class CastleUpgradeManager : MonoBehaviour
{
    [SerializeField] private CastleConfig config;
    [SerializeField] private Castle castle;

    public int HealthLevel { get; private set; }
    public int AttackSpeedLevel { get; private set; }
    public int DamageLevel { get; private set; }

    public bool CanUpgradeHealth => 
        HealthLevel < config.healthUpgrade.maxLevel;
    
    public bool CanUpgradeAttackSpeed => 
        AttackSpeedLevel < config.attackSpeedUpgrade.maxLevel;

    public bool CanUpgradeDamage =>
        DamageLevel < config.damageUpgrade.maxLevel;

    public int HealthCost => 
        CanUpgradeHealth ? config.healthUpgrade.GetCost(HealthLevel) : 0;
    
    public int AttackSpeedCost => 
        CanUpgradeAttackSpeed ? config.attackSpeedUpgrade.GetCost(AttackSpeedLevel) : 0;

    public int DamageCost =>
        CanUpgradeDamage ? config.damageUpgrade.GetCost(DamageLevel) : 0;

    public bool UpgradeHealth()
    {
        if (!CanUpgradeHealth) return false;
        if (!Bank.Instance.SpendMoney(HealthCost)) return false;

        castle.UpgradeHealth(config.healthUpgrade.valuePerLevel);
        HealthLevel++;
        return true;
    }

    public bool UpgradeAttackSpeed()
    {
        if (!CanUpgradeAttackSpeed) return false;
        if (!Bank.Instance.SpendMoney(AttackSpeedCost)) return false;

        castle.UpgradeAttackSpeed(config.attackSpeedUpgrade.valuePerLevel);
        AttackSpeedLevel++;
        return true;
    }

    public bool UpgradeDamage()
    {
        if (!CanUpgradeDamage) return false;
        if (!Bank.Instance.SpendMoney(DamageCost)) return false;

        castle.UpgradeDamage(config.damageUpgrade.valuePerLevel);
        DamageLevel++;
        return true;
    }

}
