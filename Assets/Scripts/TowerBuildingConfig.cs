using UnityEngine;

[CreateAssetMenu(fileName = "TowerBuildingConfig", menuName = "TowerDefense/BuildingConfig")]
public class TowerBuildingConfig : ScriptableObject
{
    public float minDistanceFromCastle = 4f;
    public float minDistanceBetweenTowers = 3f;
    public TowerConfig[] availableTowers;
}
