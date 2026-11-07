using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "CastleConfig", menuName = "TowerDefense/CastleConfig")]
    public class CastleConfig : ScriptableObject
    {
        [field: SerializeField]
        public float Health { get; private set; } = 500f;
        [field: SerializeField]
        public float AttackRange { get; private set; } = 8f;
        [field: SerializeField] 
        public float AttackInterval { get; private set; } = 1.5f;
        
        [field: SerializeField] 
        public float AttackDamage { get; private set; } = 20f;

        [field: SerializeField]
        public CastleUpgradeConfig HealthUpgrade { get; private set; } = new CastleUpgradeConfig
        {
            MaxLevel = 5,
            BaseCost = 20,
            CostMultiplier = 1.5f,
            ValuePerLevel = 100f
        };

        [field: SerializeField]
        public CastleUpgradeConfig AttackSpeedUpgrade { get; private set; } = new CastleUpgradeConfig
        {
            MaxLevel = 5,
            BaseCost = 30,
            CostMultiplier = 1.5f,
            ValuePerLevel = 0.2f
        };

        [field: SerializeField]
        public CastleUpgradeConfig DamageUpgrade { get; private set; } = new CastleUpgradeConfig
        {
            MaxLevel = 5,
            BaseCost = 25,
            CostMultiplier = 1.5f,
            ValuePerLevel = 5f
        };
    }
}