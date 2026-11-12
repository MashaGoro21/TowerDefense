using System;
using System.Diagnostics.Contracts;

namespace Models
{
    public class WaveModel
    {
        public int CurrentWave { get; private set; }
        public int EnemiesKilled { get; private set; }
        public int EnemiesInWave { get; private set; }
        public int EnemiesSpawned { get; private set; }
        public int TotalEnemiesKilled { get; private set; }
        public int WavesCompleted { get; private set; }

        private readonly int[] _enemiesPerWave;

        public int TotalWaves => _enemiesPerWave.Length;
        public bool IsWaveCompleted => EnemiesKilled >= EnemiesInWave;

        public WaveModel(int[] enemiesPerWave)
        {
            if (enemiesPerWave == null || enemiesPerWave.Length == 0)
                throw new ArgumentException("Wave configuration is empty.");

            _enemiesPerWave = enemiesPerWave;
        }

        public void StartWave(int waveIndex)
        {
            if (waveIndex < 0 || waveIndex >= TotalWaves)
                throw new ArgumentOutOfRangeException(nameof(waveIndex));

            CurrentWave = waveIndex + 1;
            EnemiesInWave = _enemiesPerWave[waveIndex];

            EnemiesKilled = 0;
            EnemiesSpawned = 0;
        }

        public void RegisterEnemySpawn()
        {
            EnemiesSpawned++;
        }

        public void RegisterEnemyDeath()
        {
            EnemiesKilled++;
            TotalEnemiesKilled++;
        }

        public void CompleteWave()
        {
            if (!IsWaveCompleted) return;

            WavesCompleted++;
        }
    }
}