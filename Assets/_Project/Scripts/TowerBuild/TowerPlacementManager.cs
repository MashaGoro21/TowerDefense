using UnityEngine;
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

        private Vector3 _selectedPosition;
        private List<TowerAttack> _towers = new List<TowerAttack>();
        private bool _hasSelectedPosition;
        private bool _isPaused;

        public void BuildTower(int towerIndex)
        {
            if (_buildingConfig == null || !_hasSelectedPosition || _isPaused) return;
            if (towerIndex < 0 || towerIndex >= _buildingConfig.AvailableTowers.Length) return;

            Config.TowerConfig config = _buildingConfig.AvailableTowers[towerIndex];

            if (!IsValidPosition(_selectedPosition)) return;
            if (!_bank.SpendMoney(config.Price)) return;

            GameObject tower = Instantiate(config.Prefab, _selectedPosition, Quaternion.identity);
            TowerAttack towerAttack = tower.GetComponent<TowerAttack>();
            towerAttack.SetEnemyRegistry(_enemyRegistry);
            towerAttack.SetConfig(config);

            _towers.Add(towerAttack);

            _hasSelectedPosition = false;
            OnTowerBuilt?.Invoke();
        }

        public void TrySelectGroundPosition(Vector3 position)
        {
            if (!IsValidPosition(position) || _isPaused) return;

            _selectedPosition = position;
            _hasSelectedPosition = true;

            OnBuildPositionSelected?.Invoke();
        }

        public void StopAllTowers()
        {
            foreach(TowerAttack tower in _towers)
            {
                if (tower != null) tower.StopAttack();
            }
        }

        public void StopPlacement()
        {
            _isPaused = true;
            CancelPlacement();
        }

        public void CancelPlacement()
        {
            _hasSelectedPosition = false;
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