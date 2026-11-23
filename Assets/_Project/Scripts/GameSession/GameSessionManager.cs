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
        [SerializeField] private Castle.Castle _castle;
        [SerializeField] private Towers.TowerPlacementManager _towerPlacementManager;
        [SerializeField] private Enemies.WaveManager _waveManager;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private Config.GameOverConfig _config;
        [SerializeField] private Input.PlayerInputHandler _playerInputHandler;
        [SerializeField] private Castle.CastleUpgradeManager _castleUpgradeManager;

        private RewardCalculator _rewardCalculator;

        private void Awake()
        {
            _rewardCalculator = new RewardCalculator();
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

            _waveManager.StopWaves();
            _castle.StopAttack();
            _towerPlacementManager.StopAllTowers();
            _towerPlacementManager.StopPlacement();
            _castleUpgradeManager.StopUpgrades();
            _playerInputHandler.StopInput();

            WavesCompleted = _waveManager.WavesCompleted;
            EnemiesKilled = _waveManager.TotalEnemiesKilled;

            Reward = _rewardCalculator.Calculate(
                WavesCompleted,
                EnemiesKilled,
                _config.MoneyPerWave,
                _config.MoneyPerKill
            );

            _bank.AddMoney(Reward);
            OnGameOver?.Invoke();
        }
    }
}