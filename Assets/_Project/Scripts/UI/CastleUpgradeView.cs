using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI
{
    public class CastleUpgradeView : MonoBehaviour
    {
        public event Action OnHealthClicked;
        public event Action OnAttackSpeedClicked;
        public event Action OnDamageClicked;
        public event Action OnCloseClicked;

        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _healthButton;
        [SerializeField] private Button _attackSpeedButton;
        [SerializeField] private Button _damageButton;
        [SerializeField] private Button _closeButtonPanel;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _attackSpeedText;
        [SerializeField] private TMP_Text _damageText;

        private void Awake()
        {
            _healthButton.onClick.AddListener(HandleHealthClick);
            _attackSpeedButton.onClick.AddListener(HandleAttackSpeedClick);
            _damageButton.onClick.AddListener(HandleDamageClick);
            _closeButtonPanel.onClick.AddListener(HandleCloseClick);
        }

        private void OnDestroy()
        {
            _healthButton.onClick.RemoveListener(HandleHealthClick);
            _attackSpeedButton.onClick.RemoveListener(HandleAttackSpeedClick);
            _damageButton.onClick.RemoveListener(HandleDamageClick);
            _closeButtonPanel.onClick.RemoveListener(HandleCloseClick);
        }

        private void HandleHealthClick()
        {
            OnHealthClicked?.Invoke();
        }

        private void HandleAttackSpeedClick()
        {
            OnAttackSpeedClicked?.Invoke();
        }

        private void HandleDamageClick()
        {
            OnDamageClicked?.Invoke();
        }

        private void HandleCloseClick()
        {
            OnCloseClicked?.Invoke();
        }

        public void OpenPanel()
        {
            _panel.SetActive(true);
        }

        public void ClosePanel()
        {
            _panel.SetActive(false);
        }

        public void ShowHealth(string text, bool interactable)
        {
            _healthText.text = text;
            _healthButton.interactable = interactable;
        }

        public void ShowAttackSpeed(string text, bool interactable)
        {
            _attackSpeedText.text = text;
            _attackSpeedButton.interactable = interactable;
        }

        public void ShowDamage(string text, bool interactable)
        {
            _damageText.text = text;
            _damageButton.interactable = interactable;
        }
    }
}