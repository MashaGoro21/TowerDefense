using System;
using Config;
using Economy;
using Towers;
using UI;

namespace UI
{
    public class TowerBuildPresenter : IDisposable
    {
        private readonly TowerBuildView _view;
        private readonly TowerPlacementManager _placementManager;
        private readonly TowerBuildingConfig _buildingConfig;
        private readonly Bank _bank;

        public TowerBuildPresenter(
            TowerBuildView view,
            TowerPlacementManager placementManager,
            TowerBuildingConfig buildingConfig,
            Bank bank)
        {
            _view = view;
            _placementManager = placementManager;
            _buildingConfig = buildingConfig;
            _bank = bank;

            CreateTowerButtons();

            _view.OnTowerSelected += BuildTower;
            _view.OnCloseClicked += CancelPlacement;

            _placementManager.OnBuildPositionSelected += OpenPanel;
            _placementManager.OnTowerBuilt += ClosePanel;

            _bank.OnMoneyChanged += RefreshView;
        }

        public void Dispose()
        {
            _view.OnTowerSelected -= BuildTower;
            _view.OnCloseClicked -= CancelPlacement;

            _placementManager.OnBuildPositionSelected -= OpenPanel;
            _placementManager.OnTowerBuilt -= ClosePanel;

            _bank.OnMoneyChanged -= RefreshView;
        }

        private void CreateTowerButtons()
        {
            TowerConfig[] towers = _buildingConfig.AvailableTowers;

            for (int index = 0; index < towers.Length; index++)
            {
                TowerConfig config = towers[index];
                string text = $"{config.TowerName} - {config.Price} price";
                _view.CreateTowerButton(config.Preview,text,index);
            }
        }

        private void RefreshView()
        {
            TowerConfig[] towers = _buildingConfig.AvailableTowers;

            for (int index = 0; index < towers.Length; index++)
            {
                bool canAfford = _bank.CanAfford(towers[index].Price);
                _view.SetTowerButtonInteractable(index, canAfford);
            }
        }

        private void OpenPanel()
        {
            RefreshView();
            _view.OpenPanel();
        }

        private void ClosePanel()
        {
            _view.ClosePanel();
        }

        private void CancelPlacement()
        {
            _placementManager.CancelPlacement();
            ClosePanel();
        }

        private void BuildTower(int towerIndex)
        {
            _placementManager.BuildTower(towerIndex);
        }
    }
}