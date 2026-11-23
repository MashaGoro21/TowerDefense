using UnityEngine;
using Economy;

namespace UI
{
    public class MainMenuInitializer : MonoBehaviour
    {
        [SerializeField] private MainMenuView _view;
        [SerializeField] private Bank _bank;
        [SerializeField] private string _gameSceneName = "Game";

        private MainMenuPresenter _presenter;

        private void Start()
        {
            _presenter = new MainMenuPresenter(_view, _bank, _gameSceneName);
        }

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }
    }
}