using Models;
using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class WaveManager : MonoBehaviour
    {
        public event Action OnWaveChanged;
        public event Action OnEnemiesChanged;
        public event Action OnNextWaveTimerChanged;

        public int CurrentWave => _model.CurrentWave;
        public int TotalWaves => _model.TotalWaves;
        public int EnemiesKilled => _model.EnemiesKilled;
        public int EnemiesInWave => _model.EnemiesInWave;
        public int EnemiesSpawned => _model.EnemiesSpawned;
        public int TotalEnemiesKilled => _model.TotalEnemiesKilled;
        public int WavesCompleted => _model.WavesCompleted;
        public bool IsBetweenWaves { get; private set; }
        public float TimeUntilNextWave { get; private set; }

        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private int[] _enemiesPerWave = { 5, 10, 15 };
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _timeBetweenWaves = 10f;

        private WaitForSeconds _spawnWait;
        private WaveModel _model;

        private void Awake()
        {
            _model = new WaveModel(_enemiesPerWave);
            _spawnWait = new WaitForSeconds(_spawnInterval);
        }

        private void Start()
        {
            if(!_enemySpawner.ValidateSpawnPositions())
            {
                Debug.LogError("waves cannot start: invalid spawn positions.", this);
                return;
            }

            StartCoroutine(PlayWaves());
        }

        public void RegisterEnemyDeath()
        {
            _model.RegisterEnemyDeath();
            OnEnemiesChanged?.Invoke();
        }

        private IEnumerator PlayWaves()
        {
            for (int i = 0; i < _model.TotalWaves; i++)
            {
                _model.StartWave(i);
                
                OnWaveChanged?.Invoke();
                OnEnemiesChanged?.Invoke();

                while (EnemiesSpawned < EnemiesInWave)
                {
                    Enemy enemy = _enemySpawner.SpawnEnemy();
                    enemy.OnDied += OnEnemyDied;
                    _model.RegisterEnemySpawn();

                    yield return _spawnWait;
                }

                yield return new WaitUntil(() => _model.IsWaveCompleted);
                
                _model.CompleteWave();

                if (i < _model.TotalWaves - 1)
                {
                    IsBetweenWaves = true;
                    TimeUntilNextWave = _timeBetweenWaves;

                    while (TimeUntilNextWave > 0f)
                    {
                        TimeUntilNextWave -= Time.deltaTime;
                        OnNextWaveTimerChanged?.Invoke();

                        yield return null;
                    }

                    TimeUntilNextWave = 0f;
                    IsBetweenWaves = false;
                    OnNextWaveTimerChanged?.Invoke();
                }
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= OnEnemyDied;
            _enemySpawner.UnregisterEnemy(enemy);
            RegisterEnemyDeath();
        }
    }
}