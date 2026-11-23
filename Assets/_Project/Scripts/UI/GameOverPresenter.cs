using UnityEngine.SceneManagement;
using System;
using GameSession;

namespace UI
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOverView _view;
        private readonly GameSessionManager _gameSessionManager;
        private readonly string _gameSceneName;
        private readonly string _menuSceneName;

        public GameOverPresenter(GameOverView view, GameSessionManager gameSessionManager, string gameSceneName, string menuSceneName)
        {
            _view = view;
            _gameSessionManager = gameSessionManager;
            _gameSceneName = gameSceneName;
            _menuSceneName = menuSceneName;

            _view.OnRestartClicked += Restart;
            _view.OnReturnToMenuClicked += ReturnToMenu;
            _gameSessionManager.OnGameOver += ShowGameOver;

            if (_gameSessionManager.IsGameOver) ShowGameOver();
            else _view.ClosePanel();
        }

        public void Dispose()
        {
            _view.OnRestartClicked -= Restart;
            _view.OnReturnToMenuClicked -= ReturnToMenu;
            _gameSessionManager.OnGameOver -= ShowGameOver;
        }

        private void ShowGameOver()
        {
            string wavesText = $"Waves survived: {_gameSessionManager.WavesCompleted}";

            string killsText = $"Enemies killed: {_gameSessionManager.EnemiesKilled}";

            string rewardText = $"Meta coins earned: {_gameSessionManager.Reward}";

            _view.ShowResults(wavesText, killsText, rewardText);
            _view.HideHUD();
            _view.OpenPanel();
        }

        private void Restart()
        {
            SceneManager.LoadScene(_gameSceneName);
        }

        private void ReturnToMenu()
        {
            SceneManager.LoadScene(_menuSceneName);
        }
    }
}