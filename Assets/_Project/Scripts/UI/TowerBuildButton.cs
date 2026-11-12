using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        private Action<int> OnTowerSelected;
        
        [SerializeField] private Image _preview;
        [SerializeField] private TMP_Text _towerText;
        [SerializeField] private Button _button;

        private Config.TowerConfig _config;
        private int _towerIndex;

        private void Awake()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        public void Initialize(Config.TowerConfig config, int towerIndex, Action<int> onTowerSelected)
        {
            _config = config;
            _towerIndex = towerIndex;
            OnTowerSelected = onTowerSelected;

            _preview.sprite = _config.Preview;
            _towerText.text = $"{_config.TowerName} - {_config.Price} price";
        }   
        
        public void Refresh(bool canAfford)
        {
            _button.interactable = canAfford;
        }

        private void HandleClick()
        {
            OnTowerSelected?.Invoke(_towerIndex);
        }
    }
}