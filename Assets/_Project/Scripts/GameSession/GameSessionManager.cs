using Models;
using System;
using UnityEngine;

namespace GameSession
{
    public class GameSessionManager : MonoBehaviour
    {
        public event Action OnGameOver;

        public bool IsGameOver { get; private set; }
        public int WavesCompleted { get; private set; }
        public int EnemiesKilled { get; private set; }
        public int Reward { get; private set; }

        [SerializeField] private Castle.CastleHealth _castleHealth;
        [SerializeField] private Enemies.WaveManager _waveManager;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private Config.GameOverConfig _config;

        private void Awake()
        {
            _castleHealth.OnDestroyed += HandleGameOver;
        }

        private void OnDestroy()
        {
            _castleHealth.OnDestroyed -= HandleGameOver;
        }

        private void HandleGameOver()
        {
            if (IsGameOver) return;

            IsGameOver = true;

            WavesCompleted = _waveManager.WavesCompleted;
            EnemiesKilled = _waveManager.TotalEnemiesKilled;

            Reward = RewardCalculator.Calculate(
                WavesCompleted,
                EnemiesKilled,
                _config.MoneyPerWave,
                _config.MoneyPerKill
            );

            _bank.AddMoney(Reward);

            Time.timeScale = 0f;

            OnGameOver?.Invoke();
        }
    }
}