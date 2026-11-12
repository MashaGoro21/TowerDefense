using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildUI : MonoBehaviour
    {
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Towers.TowerPlacementManager _placementManager;
        [SerializeField] private Config.TowerBuildingConfig _buildingConfig;
        [SerializeField] private TowerBuildButton _towerButtonPrefab;
        [SerializeField] private Transform _buttonsContainer;
        [SerializeField] private Button _closePanelButton;

        private readonly List<TowerBuildButton> _towerButtons = new();

        private void Awake()
        {
            _closePanelButton.onClick.AddListener(ClosePanel);

            _placementManager.OnBuildPositionSelected += OpenPanel;
            _placementManager.OnTowerBuilt += ClosePanel;
        }

        private void Start()
        {
            CreateTowerButtons();
            ClosePanel();
        }

        private void OnDestroy()
        {
            _closePanelButton.onClick.RemoveListener(ClosePanel);

            _placementManager.OnBuildPositionSelected -= OpenPanel;
            _placementManager.OnTowerBuilt -= ClosePanel;
        }

        public void OpenPanel()
        {
            _panel.SetActive(true);
            Refresh();
        }

        public void ClosePanel()
        {
            _panel.SetActive(false);
        }

        private void CreateTowerButtons()
        {
            Config.TowerConfig[] towers = _buildingConfig.AvailableTowers;

            for(int index = 0; index < towers.Length; index++)
            {
                TowerBuildButton button = Instantiate(_towerButtonPrefab, _buttonsContainer);
                button.Initialize(towers[index], index, BuildTower);
                _towerButtons.Add(button);
            }
        }

        private void BuildTower(int towerIndex)
        {
            _placementManager.BuildTower(towerIndex);
            Refresh();
        }

        private void Refresh()
        {
            Config.TowerConfig[] towers = _buildingConfig.AvailableTowers;

            for(int index = 0; index < _towerButtons.Count; index++)
            {
                bool canAfford = _bank.CanAfford(towers[index].Price);
                _towerButtons[index].Refresh(canAfford);
            }
        }
    }
}