using System;
using Economy;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputSettings;

namespace UI
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly MainMenuView _view;
        private readonly Bank _bank;
        private readonly string _gameSceneName;

        public MainMenuPresenter(MainMenuView view, Bank bank, string gameSceneName)
        {
            _view = view;
            _bank = bank;
            _gameSceneName = gameSceneName;

            _view.OnStartGameClicked += StartGame;
            _bank.OnMoneyChanged += UpdateMoney;

            UpdateMoney();
        }

        public void Dispose()
        {
            _view.OnStartGameClicked -= StartGame;
            _bank.OnMoneyChanged -= UpdateMoney;
        }

        private void UpdateMoney()
        {
            string text = _bank.Money.ToString();
            _view.ShowMoney(text);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
    }
}