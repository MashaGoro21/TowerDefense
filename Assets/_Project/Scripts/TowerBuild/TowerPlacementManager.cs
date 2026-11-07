using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

namespace Towers
{
    public class TowerPlacementManager : MonoBehaviour
    {
        public event Action OnBuildPositionSelected;
        public event Action OnTowerBuilt;

        [SerializeField] private Config.TowerBuildingConfig _buildingConfig;
        [SerializeField] private Enemies.EnemyRegistry _enemyRegistry;
        [SerializeField] private Transform _castle;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private float _maxRaycastDistance = 500f;

        private Vector3 _selectedPosition;
        private List<TowerAttack> _towers = new List<TowerAttack>();

        public void BuildTower(int towerIndex)
        {
            if (_buildingConfig == null) return;
            if (towerIndex < 0 || towerIndex >= _buildingConfig.AvailableTowers.Length) return;

            Config.TowerConfig config = _buildingConfig.AvailableTowers[towerIndex];

            if (!IsValidPosition(_selectedPosition)) return;
            if (!_bank.SpendMoney(config.Price)) return;

            GameObject tower = Instantiate(config.Prefab, _selectedPosition, Quaternion.identity);
            TowerAttack towerAttack = tower.GetComponent<TowerAttack>();
            towerAttack.SetEnemyRegistry(_enemyRegistry);
            towerAttack.SetConfig(config);

            _towers.Add(towerAttack);

            OnTowerBuilt?.Invoke();
        }

        public void TrySelectGroundPosition(Vector3 position)
        {
            if (!IsValidPosition(position)) return;

            _selectedPosition = position;
            OnBuildPositionSelected?.Invoke();
        }

        private bool IsValidPosition(Vector3 position)
        {
            if (_buildingConfig == null || _castle == null) return false;
            if (FlatDistance(position, _castle.position) < _buildingConfig.MinDistanceFromCastle)
                return false;

            foreach (TowerAttack tower in _towers)
            {
                if (tower == null) continue;

                if (FlatDistance(position, tower.transform.position) < _buildingConfig.MinDistanceBetweenTowers)
                    return false;
            }

            return true;
        }

        private float FlatDistance(Vector3 a, Vector3 b)
        {
            a.y = 0f;
            b.y = 0f;

            return Vector3.Distance(a, b);
        }

    }
}