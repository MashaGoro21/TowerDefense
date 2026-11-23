using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildView : MonoBehaviour
    {
        public event Action<int> OnTowerSelected;
        public event Action OnCloseClicked;

        [SerializeField] private GameObject _panel;
        [SerializeField] private TowerBuildButton _towerButtonPrefab;
        [SerializeField] private Transform _buttonsContainer;
        [SerializeField] private Button _closePanelButton;

        private readonly List<TowerBuildButton> _towerButtons = new();

        private void Awake()
        {
            _closePanelButton.onClick.AddListener(HandleCloseClick);
        }

        private void OnDestroy()
        {
            _closePanelButton.onClick.RemoveListener(HandleCloseClick);

            foreach(TowerBuildButton button in _towerButtons)
            {
                if (button != null) button.OnTowerSelected -= HandleTowerSelected;
            }
        }

        public void CreateTowerButton(Sprite preview, string text, int towerIndex)
        {
            TowerBuildButton button = Instantiate(_towerButtonPrefab, _buttonsContainer);
            
            button.Initialize(preview, text, towerIndex);
            button.OnTowerSelected += HandleTowerSelected;
            
            _towerButtons.Add(button);
        }

        public void SetTowerButtonInteractable(int towerIndex, bool interactable)
        {
            _towerButtons[towerIndex].SetInteractable(interactable);
        }

        public void OpenPanel()
        {
            _panel.SetActive(true);
        }

        public void ClosePanel()
        {
            _panel.SetActive(false);
        }

        private void HandleTowerSelected(int towerIndex)
        {
            OnTowerSelected?.Invoke(towerIndex);
        }

        private void HandleCloseClick()
        {
            OnCloseClicked?.Invoke();
        }
    }
}