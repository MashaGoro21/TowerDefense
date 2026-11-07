using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "TowerBuildingConfig", menuName = "TowerDefense/BuildingConfig")]
    public class TowerBuildingConfig : ScriptableObject
    {
        [field: SerializeField]
        public float MinDistanceFromCastle { get; private set; } = 4f;
        [field: SerializeField]
        public float MinDistanceBetweenTowers { get; private set; } = 3f;
        [field: SerializeField]
        public TowerConfig[] AvailableTowers { get; private set; }
    }
}