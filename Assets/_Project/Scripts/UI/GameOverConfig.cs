using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "GameOverConfig", menuName = "TowerDefense/GameOverConfig")]
    public class GameOverConfig : ScriptableObject
    {
        [field: SerializeField]
        public int MoneyPerWave { get; private set; } = 10;
        [field: SerializeField]
        public int MoneyPerKill { get; private set; } = 2;
    }
}
