using UnityEngine;

namespace Config
{
    [System.Serializable]
    public class CastleUpgradeConfig
    {
        public int MaxLevel = 5;
        public int BaseCost = 20;
        public float CostMultiplier = 1.5f;
        public float ValuePerLevel = 10f;

        public int GetCost(int currentLevel)
        {
            return Mathf.CeilToInt(BaseCost * Mathf.Pow(CostMultiplier, currentLevel));
        }
    }
}