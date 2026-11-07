using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "TowerDefense/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [field: SerializeField]
        public string TowerName { get; private set; }
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
        [field: SerializeField]
        public Sprite Preview { get; private set; }
        [field: SerializeField]
        public int Price { get; private set; } = 50;

        [field: SerializeField]
        public float AttackRange { get; private set; } = 5f;
        [field: SerializeField]
        public float AttackInterval { get; private set; } = 1f;
        [field: SerializeField]
        public float AttackDamage { get; private set; } = 10f;
    }
}
