using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class WaveManager : MonoBehaviour
    {
        public int CurrentWave { get; private set; }
        public int TotalWaves => _enemiesPerWave.Length;
        public int EnemiesKilled { get; private set; }
        public int EnemiesInWave { get; private set; }
        public int EnemiesSpawned { get; private set; }
        public bool IsBetweenWaves { get; private set; }
        public float TimeUntilNextWave { get; private set; }
        public int TotalEnemiesKilled { get; private set; }
        public int WavesCompleted { get; private set; }
        
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private EnemyRegistry _enemyRegistry;
        [SerializeField] private Castle.CastleHealth _castleHealth;
        [SerializeField] private Transform _castleTarget;
        [SerializeField] private int[] _enemiesPerWave = { 5, 10, 15 };
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _timeBetweenWaves = 10f;
        [SerializeField] private float _maxSpawnSearchDistance = 3f;

        private WaitForSeconds _spawnWait;

        private void Awake()
        {
            _spawnWait = new WaitForSeconds(_spawnInterval);
        }

        private void Start()
        {
            StartCoroutine(PlayWaves());
        }

        public void RegisterEnemyDeath()
        {
            EnemiesKilled++;
            TotalEnemiesKilled++;
        }

        private IEnumerator PlayWaves()
        {
            TotalEnemiesKilled = 0;
            WavesCompleted = 0;

            for (int i = 0; i < _enemiesPerWave.Length; i++)
            {
                CurrentWave = i + 1;
                EnemiesInWave = _enemiesPerWave[i];
                EnemiesKilled = 0;
                EnemiesSpawned = 0;

                while (EnemiesSpawned < EnemiesInWave)
                {
                    if (SpawnEnemy()) EnemiesSpawned++;

                    yield return _spawnWait;
                }

                yield return new WaitUntil(() => EnemiesKilled >= EnemiesInWave);
                WavesCompleted++;

                if (i < _enemiesPerWave.Length - 1)
                {
                    IsBetweenWaves = true;
                    TimeUntilNextWave = _timeBetweenWaves;

                    while (TimeUntilNextWave > 0f)
                    {
                        TimeUntilNextWave -= Time.deltaTime;
                        yield return null;
                    }

                    TimeUntilNextWave = 0f;
                    IsBetweenWaves = false;
                }
            }
        }

        private bool SpawnEnemy()
        {
            int index = Random.Range(0, _spawnPoints.Length);

            Transform pointA = _spawnPoints[index];
            Transform pointB = _spawnPoints[(index + 1) % _spawnPoints.Length];

            Vector3 position = Vector3.Lerp(pointA.position, pointB.position, Random.value);

            if (NavMesh.SamplePosition(position, out NavMeshHit hit, _maxSpawnSearchDistance, NavMesh.AllAreas))
            {
                GameObject enemyObject = Instantiate(_enemyPrefab, hit.position, Quaternion.identity);
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                enemy.Initialize(_bank, _castleHealth, _castleTarget);
                _enemyRegistry.RegisterEnemy(enemy);
                enemy.OnDied += OnEnemyDied;
                return true;
            }

            return false;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= OnEnemyDied;
            _enemyRegistry.UnregisterEnemy(enemy);
            RegisterEnemyDeath();
        }
    }
}