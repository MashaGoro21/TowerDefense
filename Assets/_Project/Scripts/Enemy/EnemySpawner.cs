using Enemies;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private EnemyRegistry _enemyRegistry;
        [SerializeField] private Castle.CastleHealth _castleHealth;
        [SerializeField] private Transform _castleTarget;
        [SerializeField] private float _spawnValidationStep = 0.25f;
        [SerializeField] private float _maxSpawnOffset = 0.1f;

        public Enemy SpawnEnemy()
        {
            int index = Random.Range(0, _spawnPoints.Length);

            Transform pointA = _spawnPoints[index];
            Transform pointB = _spawnPoints[(index + 1) % _spawnPoints.Length];

            Vector3 position = Vector3.Lerp(pointA.position, pointB.position, Random.value);

            if (!NavMesh.SamplePosition(position, out NavMeshHit hit, _maxSpawnOffset, NavMesh.AllAreas))
                throw new System.InvalidOperationException("Enemy spawn position is outside NavMesh.");

            GameObject enemyObject = Instantiate(_enemyPrefab, hit.position, Quaternion.identity);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.Initialize(_bank, _castleHealth, _castleTarget);
            _enemyRegistry.RegisterEnemy(enemy);

            return enemy;
        }

        public bool ValidateSpawnPositions()
        {
            if (_spawnPoints == null || _spawnPoints.Length < 2) return false;
            if (_spawnValidationStep <= 0f || _maxSpawnOffset <= 0f) return false;

            for (int index = 0; index < _spawnPoints.Length; index++)
            {
                Transform pointA = _spawnPoints[index];
                Transform pointB = _spawnPoints[(index + 1) % _spawnPoints.Length];

                float distance = Vector3.Distance(pointA.position, pointB.position);

                int steps = Mathf.Max(1, Mathf.CeilToInt(distance / _spawnValidationStep));

                for (int step = 0; step <= steps; step++)
                {
                    float t = (float)step / steps;
                    Vector3 position = Vector3.Lerp(pointA.position, pointB.position, t);

                    if (!NavMesh.SamplePosition(position, out NavMeshHit hit, _maxSpawnOffset, NavMesh.AllAreas))
                        return false;
                }
            }

            return true;
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            _enemyRegistry.UnregisterEnemy(enemy);
        }
    }
}