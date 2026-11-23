using UnityEngine;
using GameSession;

namespace UI
{
    public class GameOverInitializer : MonoBehaviour
    {
        [SerializeField] private GameOverView _view;
        [SerializeField] private GameSessionManager _gameSessionManager;
        [SerializeField] private string _gameSceneName = "Game";
        [SerializeField] private string _menuSceneName = "MainMenu";

        private GameOverPresenter _presenter;

        private void Start()
        {
            _presenter = new GameOverPresenter(_view, _gameSessionManager, _gameSceneName, _menuSceneName);
        }

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }
    }
}