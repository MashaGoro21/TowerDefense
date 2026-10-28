using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class CastleUpgradeConfig
{
    public int maxLevel = 5;
    public int baseCost = 20;
    public float costMultiplier = 1.5f;
    public float valuePerLevel = 10f;

    public int GetCost(int currentLevel)
    {
        return Mathf.CeilToInt(baseCost * Mathf.Pow(costMultiplier, currentLevel));
    }
}

[CreateAssetMenu(fileName = "CastleConfig", menuName = "TowerDefense/CastleConfig")]
public class CastleConfig : ScriptableObject
{
    public float health = 500f;
    public float attackRange = 8f;
    public float attackInterval = 1.5f;
    public float attackDamage = 20f;

    public CastleUpgradeConfig healthUpgrade = new CastleUpgradeConfig
    {
        maxLevel = 5,
        baseCost = 20,
        costMultiplier = 1.5f,
        valuePerLevel = 100f
    };

    public CastleUpgradeConfig attackSpeedUpgrade = new CastleUpgradeConfig
    {
        maxLevel = 5,
        baseCost = 30,
        costMultiplier = 1.5f,
        valuePerLevel = 0.2f
    };

    public CastleUpgradeConfig damageUpgrade = new CastleUpgradeConfig
    {
        maxLevel = 5,
        baseCost = 25,
        costMultiplier = 1.5f,
        valuePerLevel = 5f
    };
}
