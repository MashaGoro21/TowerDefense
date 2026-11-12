using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameSession.GameSessionManager _gameSessionManager;
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

            _gameSessionManager.OnGameOver += ShowGameOver;
        }

        private void Start()
        {
            if (_gameOverPanel != null) _gameOverPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(Restart);
            _returnToMenuButton.onClick.RemoveListener(ReturnToMenu);

            _gameSessionManager.OnGameOver -= ShowGameOver;
        }

        public void ShowGameOver()
        {
            _wavesText.text = $"Waves survived: {_gameSessionManager.WavesCompleted}";
            _killsText.text = $"Enemies killed: {_gameSessionManager.EnemiesKilled}";
            _rewardText.text = $"Meta coins earned: {_gameSessionManager.Reward}";

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