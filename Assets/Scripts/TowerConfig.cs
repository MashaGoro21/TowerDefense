using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "TowerDefense/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public string towerName;
    public GameObject prefab;
    public Sprite preview;
    public int price = 50;

    public float attackRange = 5f;
    public float attackInterval = 1f;
    public float attackDamage = 10f;
}
