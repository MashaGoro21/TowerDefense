using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class GameOverView : MonoBehaviour
    {
        public event Action OnRestartClicked;
        public event Action OnReturnToMenuClicked;

        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _gameHUD;
        [SerializeField] private TMP_Text _wavesText;
        [SerializeField] private TMP_Text _killsText;
        [SerializeField] private TMP_Text _rewardText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _returnToMenuButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(HandleRestartClick);
            _returnToMenuButton.onClick.AddListener(HandleReturnToMenuClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(HandleRestartClick);
            _returnToMenuButton.onClick.RemoveListener(HandleReturnToMenuClick);
        }

        public void ShowResults(string wavesText, string killsText, string rewardText)
        {
            _wavesText.text = wavesText;
            _killsText.text = killsText;
            _rewardText.text = rewardText;
        }

        public void OpenPanel()
        {
            _gameOverPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            _gameOverPanel.SetActive(false);
        }

        public void HideHUD()
        {
            _gameHUD.SetActive(false);
        }

        private void HandleRestartClick()
        {
            OnRestartClicked?.Invoke();
        }

        private void HandleReturnToMenuClick()
        {
            OnReturnToMenuClicked?.Invoke();
        }
    }
}