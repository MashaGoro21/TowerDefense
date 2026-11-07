using TMPro;
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
        [SerializeField] private Image _fastTowerPreview;
        [SerializeField] private TMP_Text _fastTowerText;
        [SerializeField] private Button _fastTowerButton;
        [SerializeField] private Image _heavyTowerPreview;
        [SerializeField] private TMP_Text _heavyTowerText;
        [SerializeField] private Button _heavyTowerButton;
        [SerializeField] private Button _closePanelButton;

        private void Awake()
        {
            _fastTowerButton.onClick.AddListener(BuildFastTower);
            _heavyTowerButton.onClick.AddListener(BuildHeavyTower);
            _closePanelButton.onClick.AddListener(ClosePanel);

            _placementManager.OnBuildPositionSelected += OpenPanel;
            _placementManager.OnTowerBuilt += ClosePanel;
        }

        private void Start()
        {
            ClosePanel();
            Refresh();
        }

        private void OnDestroy()
        {
            _fastTowerButton.onClick.RemoveListener(BuildFastTower);
            _heavyTowerButton.onClick.RemoveListener(BuildHeavyTower);
            _closePanelButton.onClick.RemoveListener(ClosePanel);

            _placementManager.OnBuildPositionSelected -= OpenPanel;
            _placementManager.OnTowerBuilt -= ClosePanel;
        }

        private void Update()
        {
            if (_panel != null && _panel.activeSelf) Refresh();
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

        public void BuildFastTower()
        {
            _placementManager.BuildTower(0);
            Refresh();
        }

        public void BuildHeavyTower()
        {
            _placementManager.BuildTower(1);
            Refresh();
        }

        private void Refresh()
        {
            if (_buildingConfig == null) return;

            Config.TowerConfig[] towers = _buildingConfig.AvailableTowers;

            UpdateTowerButton(towers[0], _fastTowerPreview, _fastTowerText, _fastTowerButton);
            UpdateTowerButton(towers[1], _heavyTowerPreview, _heavyTowerText, _heavyTowerButton);
        }

        private void UpdateTowerButton(
            Config.TowerConfig config,
            Image preview,
            TMP_Text towerText,
            Button button)
        {
            bool canAfford = _bank.Money >= config.Price;

            if (preview != null) preview.sprite = config.Preview;

            if (towerText != null)
                towerText.text = $"{config.TowerName} - {config.Price} price";

            if (button != null) button.interactable = canAfford;
        }
    }
}