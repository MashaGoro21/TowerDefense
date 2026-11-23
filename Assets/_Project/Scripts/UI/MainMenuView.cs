using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnStartGameClicked;

        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private Button _startGameButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(HandleStartGameClick);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(HandleStartGameClick);
        }

        public void ShowMoney(string text)
        {
            _moneyText.text = text;
        }

        private void HandleStartGameClick()
        {
            OnStartGameClicked?.Invoke();
        }
    }
}