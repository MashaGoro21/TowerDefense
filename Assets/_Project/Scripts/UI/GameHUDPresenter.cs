using System;
using UnityEngine;
using Enemies;
using Economy;
using Castle;

namespace UI
{
    public class GameHUDPresenter : IDisposable
    {
        private readonly GameHUDView _view;
        private readonly WaveManager _waveManager;
        private readonly Bank _bank;
        private readonly CastleHealth _castleHealth;

        public GameHUDPresenter(GameHUDView view, WaveManager waveManager, Bank bank, CastleHealth castleHealth)
        {
            _view = view;
            _waveManager = waveManager;
            _bank = bank;
            _castleHealth = castleHealth;

            _waveManager.OnWaveChanged += UpdateWave;
            _waveManager.OnEnemiesChanged += UpdateEnemies;
            _waveManager.OnNextWaveTimerChanged += UpdateNextWaveTimer;
            _bank.OnMoneyChanged += UpdateMoney;
            _castleHealth.OnHealthChanged += UpdateCastleHP;

            UpdateWave();
            UpdateEnemies();
            UpdateMoney();
            UpdateCastleHP();
            UpdateNextWaveTimer();
        }

        public void Dispose()
        {
            _waveManager.OnWaveChanged -= UpdateWave;
            _waveManager.OnEnemiesChanged -= UpdateEnemies;
            _waveManager.OnNextWaveTimerChanged -= UpdateNextWaveTimer;
            _bank.OnMoneyChanged -= UpdateMoney;
            _castleHealth.OnHealthChanged -= UpdateCastleHP;
        }

        private void UpdateWave()
        {
            string text = $"Wave {_waveManager.CurrentWave}/{_waveManager.TotalWaves}";
            _view.ShowWave(text);
        }

        private void UpdateEnemies()
        {
            string text = $"Enemies {_waveManager.EnemiesKilled}/{_waveManager.EnemiesInWave}";
            _view.ShowEnemies(text);
        }

        private void UpdateMoney()
        {
            string text = _bank.Money.ToString();
            _view.ShowMoney(text);
        }

        private void UpdateCastleHP()
        {
            string text = $"Castle {_castleHealth.CurrentHealth}/{_castleHealth.MaxHealth}";
            _view.ShowCastleHP(text);
        }

        private void UpdateNextWaveTimer()
        {
            bool visible = _waveManager.IsBetweenWaves;
            int seconds = Mathf.CeilToInt(_waveManager.TimeUntilNextWave);
            string text = $"Next wave in {seconds} sec";
            _view.ShowNextWaveTimer(visible, text);
        }
    }
}