using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        public Action<int> OnTowerSelected;
        
        [SerializeField] private Image _preview;
        [SerializeField] private TMP_Text _towerText;
        [SerializeField] private Button _button;

        private int _towerIndex;

        private void Awake()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        public void Initialize(Sprite preview, string text, int towerIndex)
        {
            _towerIndex = towerIndex;
            _preview.sprite = preview;
            _towerText.text = text;
        }   
        
        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        private void HandleClick()
        {
            OnTowerSelected?.Invoke(_towerIndex);
        }
    }
}