using UnityEngine;
using Config;
using Economy;
using Towers;

namespace UI
{
    public class TowerBuildInitializer : MonoBehaviour
    {
        [SerializeField] private TowerBuildView _view;
        [SerializeField] private TowerPlacementManager _placementManager;
        [SerializeField] private TowerBuildingConfig _buildingConfig;
        [SerializeField] private Bank _bank;

        private TowerBuildPresenter _presenter;

        private void Start()
        {
            _presenter = new TowerBuildPresenter(_view, _placementManager, _buildingConfig, _bank);
        }

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }
    }
}