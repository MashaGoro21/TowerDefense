using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverManager : MonoBehaviour
    {
        public bool IsGameOver { get; private set; }
        
        [SerializeField] private Config.GameOverConfig _config;
        [SerializeField] private Enemies.WaveManager _waveManager;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _gameHUD;
        [SerializeField] private TMP_Text _wavesText;
        [SerializeField] private TMP_Text _killsText;
        [SerializeField] private TMP_Text _rewardText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _returnToMenuButton;
        [SerializeField] private string _gameSceneName = "Game";
        [SerializeField] private string _menuSceneName = "MainMenu";

        private void Awake()
        {
            _restartButton.onClick.AddListener(Restart);
            _returnToMenuButton.onClick.AddListener(ReturnToMenu);
        }

        private void Start()
        {
            if (_gameOverPanel != null) _gameOverPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(Restart);
            _returnToMenuButton.onClick.RemoveListener(ReturnToMenu);
        }

        public void ShowGameOver()
        {
            if (IsGameOver) return;

            IsGameOver = true;

            Time.timeScale = 0f;

            int waves = _waveManager != null ? _waveManager.WavesCompleted : 0;
            int kills = _waveManager != null ? _waveManager.TotalEnemiesKilled : 0;

            int reward = 0;

            if (_config != null)
                reward = waves * _config.MoneyPerWave + kills * _config.MoneyPerKill;

            if (_bank != null) _bank.AddMoney(reward);

            if (_wavesText != null)
                _wavesText.text = $"Waves survived: {waves}";

            if (_killsText != null)
                _killsText.text = $"Enemies killed: {kills}";

            if (_rewardText != null)
                _rewardText.text = $"Meta coins earned: {reward}";

            if (_gameHUD != null)
                _gameHUD.SetActive(false);

            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_gameSceneName);
        }

        public void ReturnToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_menuSceneName);
        }
    }
}